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

namespace BasicIC.Services.Implement
{
    public class OrderService : BaseCRUDService<OrderModel, M03_Order>, IOrderService
    {
        protected ICartService _cartService;
        protected IOrderDetailService _orderDetailService;
        protected ICartDetailService _cartDetailService;
        public OrderService(BasicICRepository<M03_Order> repo,
            ICartService cartService,
            IOrderDetailService orderDetailService,
            ICartDetailService cartDetailService,
            ILogger logger, IConfigManager config, IMapper mapper) : base(repo, config, logger, mapper)
        {
            _cartService = cartService;
            _orderDetailService = orderDetailService;
            _cartDetailService= cartDetailService;
            _cartService= cartService;
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
    }
}