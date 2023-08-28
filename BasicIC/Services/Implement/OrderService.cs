using AutoMapper;
using BasicIC.Interfaces;
using BasicIC.KafkaComponents;
using BasicIC.Models.Common;
using BasicIC.Models.Main;
using BasicIC.Models.Main.M03;
using BasicIC.Services.Interfaces;
using Common;
using Common.Commons;
using Common.Interfaces;
using Common.Params.Base;
using Repository.CustomModel;
using Repository.EF;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicIC.Services.Implement
{
    public class OrderService : BaseCRUDService<OrderModel, M03_Order>, IOrderService
    {
        private readonly ICartService _cartService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly ICartDetailService _cartDetailService;
        private readonly IProductService _productService;
        private readonly IAddressService _addressService;
        //private readonly ISendEmailService _sendEmailService;
        private readonly ICustomerService _customerService;
        private readonly BasicICRepository<M03_OrderDetail> _repoOrderDetail;

        public OrderService(BasicICRepository<M03_Order> repo,
            ICartService cartService,
            BasicICRepository<M03_OrderDetail> repoOrderDetail,
            IAddressService addressService,
            IOrderDetailService orderDetailService,
            ICartDetailService cartDetailService,
            ISendEmailService sendEmailService,
            ICustomerService customerService,
            IProductService productService,
            ILogger logger, IConfigManager config, IMapper mapper) : base(repo, config, logger, mapper)
        {
            _addressService = addressService;
            _cartService = cartService;
            _orderDetailService = orderDetailService;
            _cartDetailService = cartDetailService;
            _repoOrderDetail = repoOrderDetail;
            //_sendEmailService = sendEmailService;
            _customerService = customerService;
            _productService = productService;
        }

        public async Task<ResponseService<ListResult<OrderOrderDetailModel>>> GetAllByCustomer(CustomerModel param, M03_BasicEntities dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                List<OrderOrderDetailModel> res = new List<OrderOrderDetailModel>();
                List<OrderModel> listOrderModel = (await this.GetByCustomerID(param)).data.items;
                foreach (OrderModel orderModel in listOrderModel)
                {
                    OrderOrderDetailModel orderOrderDetailModel = new OrderOrderDetailModel(orderModel, (await _orderDetailService.GetByOrderID(orderModel)).data.items);
                    res.Add(orderOrderDetailModel);
                }
                return new ResponseService<ListResult<OrderOrderDetailModel>>(new ListResult<OrderOrderDetailModel>(res, res.Count));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<OrderOrderDetailModel>>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
            }
        }

        public async Task<ResponseService<OrderOrderDetailModel>> GetOrderOrderDetail(OrderModel obj, M03_BasicEntities dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                OrderModel orderModel = _mapper.Map<M03_Order, OrderModel>((await _repo.GetById(obj.id)));
                List<OrderDetailModel> listOrderDetailModel = (await _orderDetailService.GetByOrderID(orderModel)).data.items;
                OrderOrderDetailModel res = new OrderOrderDetailModel(orderModel, listOrderDetailModel);
                return new ResponseService<OrderOrderDetailModel>(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<OrderOrderDetailModel>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
            }
        }

        public async Task<ResponseService<bool>> DeleteRelatives(OrderModel param, M03_BasicEntities dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                //var entity = await _repo.GetById(param.id);
                ResponseService<bool> a = await _orderDetailService.DeleteByOrder(param, dbContext);
                M03_Order result = await _repo.Delete(param.id, dbContext);


                if (result != null)
                {
                    return new ResponseService<bool>(true);
                }
                else
                    return new ResponseService<bool>(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<bool>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
            }
        }

        public async Task<ResponseService<OrderOrderDetailModel>> UpdateOrderOrderDetail(OrderOrderDetailModel obj, M03_BasicEntities dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                M03_Order orderDb = await _repo.GetById(obj.id, dbContext);
                var listOrderDetailRaw = await _orderDetailService.GetByOrderID(_mapper.Map<M03_Order, OrderModel>(orderDb));
                List<OrderDetailModel> listOrderDetail = listOrderDetailRaw.data.items;
                OrderModel TResultDb;
                try
                {
                    TResultDb = _mapper.Map<M03_Order, OrderModel>(orderDb);
                }
                catch
                {
                    return new ResponseService<OrderOrderDetailModel>(Constants.ERROR_MAPPING_MODEL).BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
                }
                obj.UpdateInfo(TResultDb);

                M03_Order VResultDb;
                try
                {
                    VResultDb = _mapper.Map(obj.CreateOrderModel(), orderDb);
                }
                catch
                {
                    return new ResponseService<OrderOrderDetailModel>(Constants.ERROR_MAPPING_MODEL).BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
                }
                // update master

                List<string> fieldsformaster = new List<string>
                {
                    nameof(OrderModel.addresses_id), nameof(OrderModel.customer_phone_number), nameof(OrderModel.total_price), nameof(OrderModel.total_price_coupon), nameof(OrderModel.payment_method),nameof(OrderModel.arrived_date), nameof(OrderModel.shipping_address), nameof(OrderModel.shipping_status), nameof(OrderModel.shipping_fee), nameof(OrderModel.order_payment_collection), nameof(OrderModel.note)
                };

                M03_Order result = await _repo.UpdateFields(VResultDb, fieldsformaster, dbContext);

                // update detail
                List<string> fieldsfordetail = new List<string>
                {
                    nameof(OrderDetailModel.quantity), nameof(OrderDetailModel.total_price)
                };
                List<M03_OrderDetail> listresult = new List<M03_OrderDetail>();
                foreach (var orderDetail in obj.orderDetailModel)
                {
                    var resulttmp = await _repoOrderDetail.UpdateFields(_mapper.Map<OrderDetailModel, M03_OrderDetail>(orderDetail), fieldsfordetail, dbContext);
                    listresult.Add(resulttmp);
                }

                OrderOrderDetailModel res = new OrderOrderDetailModel(_mapper.Map<M03_Order, OrderModel>(result), obj.orderDetailModel);

                return new ResponseService<OrderOrderDetailModel>(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<OrderOrderDetailModel>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
            }
        }

        public async Task<ResponseService<OrderModel>> CreateFromCart(OrderModel param1, M03_BasicEntities dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                param1.AddInfo();
                M03_Order vData;
                try
                {
                    vData = _mapper.Map<OrderModel, M03_Order>(param1);
                }
                catch (Exception ex)
                {
                    return new ResponseService<OrderModel>(ex.Message).BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
                }
                ResponseService<CartModel> cartModel = await _cartService.GetByCustomerID(param1);
                M03_Order result = await _repo.Create(vData, dbContext);

                OrderDetailModel itemorder;

                PagingParam a = new PagingParam();

                ResponseService<ListResult<CartDetailModel>> listCartDetailModel = await _cartDetailService.GetByCartID(cartModel.data);

                foreach (var itemcartdetail in listCartDetailModel.data.items)
                {
                    itemorder = new OrderDetailModel();
                    itemorder.AddInfo();
                    itemorder.order_id = param1.id;
                    itemorder.product_id = itemcartdetail.product_id;
                    itemorder.quantity = itemcartdetail.quantity;
                    itemorder.total_price = itemcartdetail.cart_total_price;
                    itemorder.product_price = (await _productService.GetById(new ItemModel(itemcartdetail.product_id))).data.price;
                    await _orderDetailService.Create(itemorder);
                    param1.total_price += itemorder.quantity * itemorder.product_price;
                }
                param1.total_price += param1.shipping_fee;
                var addressModel = (await _addressService.GetById(new ItemModel(param1.addresses_id))).data;
                param1.shipping_address = addressModel.address_line1 + " " + addressModel.address_line2;
                CustomerModel customerModel = (await _customerService.GetById(new ItemModel(param1.customer_id))).data;
                ProducerWrapper<OrderConfirmEmailModel> _producer = new ProducerWrapper<OrderConfirmEmailModel>();
                await _producer.CreateMess(Topic.SEND_EMAIL, new OrderConfirmEmailModel(param1.id.ToString(), customerModel.customer_name, customerModel.phone_number, param1.total_price, param1.shipping_address, param1.shipping_fee, (await _orderDetailService.GetByOrderID(param1)).data.items, customerModel.email, "THÔNG BÁO TẠO ĐƠN HÀNG THÀNH CÔNG"));

                return new ResponseService<OrderModel>(_mapper.Map<M03_Order, OrderModel>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<OrderModel>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
            }
        }
        public async Task<ResponseService<ListResult<OrderModel>>> GetByCustomerID(CustomerModel param, M03_BasicEntities dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                // Get result from Entity
                ListResult<M03_Order> resultEntity = await _repo.FindAsyncWithField(nameof(OrderModel.customer_id), param.id, dbContext);

                // Map result to View
                List<OrderModel> items;
                try
                {
                    items = _mapper.Map<List<M03_Order>, List<OrderModel>>(resultEntity.items);

                }
                catch
                {
                    return new ResponseService<ListResult<OrderModel>>(Constants.ERROR_MAPPING_MODEL).BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
                }

                ListResult<OrderModel> result = new ListResult<OrderModel>(items, resultEntity.total);

                return new ResponseService<ListResult<OrderModel>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<OrderModel>>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
            }
        }
    }
}