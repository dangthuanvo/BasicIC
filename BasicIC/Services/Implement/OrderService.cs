using AutoMapper;
using BasicIC.Interfaces;
using BasicIC.Models.Main.M03;
using BasicIC.Services.Interfaces;
using Common.Commons;
using Common;
using Common.Interfaces;
using Repository.EF;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Common.Params.Base;
using Repository.CustomModel;
using System.Data.Entity;
using System.Xml.Linq;

namespace BasicIC.Services.Implement
{
    public class OrderService : BaseCRUDService<OrderModel, M03_Order>, IOrderService
    {
        private readonly ICartService _cartService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly ICartDetailService _cartDetailService;
        private readonly BasicICRepository<M03_OrderDetail> _repoOrderDetail;

        public OrderService(BasicICRepository<M03_Order> repo,
            ICartService cartService,
            BasicICRepository<M03_OrderDetail> repoOrderDetail,
            IOrderDetailService orderDetailService,
            ICartDetailService cartDetailService,
            ILogger logger, IConfigManager config, IMapper mapper) : base(repo, config, logger, mapper)
        {
            _cartService = cartService;
            _orderDetailService = orderDetailService;
            _cartDetailService= cartDetailService;
            _cartService= cartService;
            _repoOrderDetail = repoOrderDetail;
        }

        public async Task<ResponseService<ListResult<OrderMasterModel>>> GetAllByCustomer(CustomerModel param, M03_BasicEntities dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                List<OrderMasterModel> res = new List<OrderMasterModel>();
                List<OrderModel> listOrderModel = (await this.GetByCustomerID(param)).data.items;
                foreach(OrderModel orderModel in listOrderModel)
                {
                    OrderMasterModel orderMasterModel = new OrderMasterModel(orderModel, (await _orderDetailService.GetByOrderID(orderModel)).data.items);
                    //orderMasterModel.Create(orderModel,(await _orderDetailService.GetByOrderID(orderModel)).data.items);
                    res.Add(orderMasterModel);
                }
                return new ResponseService<ListResult<OrderMasterModel>>(new ListResult<OrderMasterModel>(res, res.Count));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<OrderMasterModel>>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
            }
        }

        public async Task<ResponseService<OrderMasterModel>> GetMaster(OrderModel obj, M03_BasicEntities dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                OrderModel orderModel = _mapper.Map<M03_Order, OrderModel>((await _repo.GetById(obj.id)));
                List<OrderDetailModel> listOrderDetailModel = (await _orderDetailService.GetByOrderID(orderModel)).data.items;
                OrderMasterModel res = new OrderMasterModel(orderModel, listOrderDetailModel);
                //res.Create(orderModel, listOrderDetailModel);
                return new ResponseService<OrderMasterModel>(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<OrderMasterModel>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
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

        public async Task<ResponseService<OrderMasterModel>> UpdateMaster(OrderMasterModel obj, M03_BasicEntities dbContext = null)
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
                    return new ResponseService<OrderMasterModel>(Constants.ERROR_MAPPING_MODEL).BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
                }
                obj.UpdateInfo(TResultDb);

                M03_Order VResultDb;
                try
                {
                    VResultDb = _mapper.Map(obj.CreateOrderModel() , orderDb);
                }
                catch
                {
                    return new ResponseService<OrderMasterModel>(Constants.ERROR_MAPPING_MODEL).BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
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

                OrderMasterModel res = new OrderMasterModel(_mapper.Map<M03_Order, OrderModel>(result), obj.orderDetailModel);

                //res.Create(_mapper.Map<M03_Order, OrderModel>(result), obj.orderDetailModel);
                return new ResponseService<OrderMasterModel>(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<OrderMasterModel>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
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
                    await _orderDetailService.Create(itemorder);
                }
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