using AutoMapper;
using BasicIC.Interfaces;
using BasicIC.Models.Common;
using BasicIC.Models.Main.M03;
using BasicIC.Services.Interfaces;
using Common;
using Common.Commons;
using Common.Interfaces;
using Repository.CustomModel;
using Repository.EF;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BasicIC.Services.Implement
{
    public class OrderDetailService : BaseCRUDService<OrderDetailModel, M03_OrderDetail>, IOrderDetailService
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;

        public OrderDetailService(BasicICRepository<M03_OrderDetail> repo,
            ICartService cartService,
            IProductService productService,
        ILogger logger, IConfigManager config, IMapper mapper) : base(repo, config, logger, mapper)
        {
            _cartService = cartService;
            _productService = productService;
        }

        public async Task<ResponseService<bool>> DeleteByOrder(OrderModel param, M03_BasicEntities dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                List<M03_OrderDetail> results = await _repo.DeleteAsyncWithField(nameof(OrderDetailModel.order_id), param.id, dbContext);

                if (results.Count > 0)
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

        public async Task<ResponseService<ListResult<OrderDetailModel>>> GetByOrderID(OrderModel param, M03_BasicEntities dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                // Get result from Entity
                ListResult<M03_OrderDetail> resultEntity = await _repo.FindAsyncWithField(nameof(OrderDetailModel.order_id), param.id, dbContext);

                // Map result to View
                List<OrderDetailModel> items;
                try
                {
                    items = _mapper.Map<List<M03_OrderDetail>, List<OrderDetailModel>>(resultEntity.items);
                }
                catch
                {
                    return new ResponseService<ListResult<OrderDetailModel>>(Constants.ERROR_MAPPING_MODEL).BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
                }

                ListResult<OrderDetailModel> result = new ListResult<OrderDetailModel>(items, resultEntity.total);

                foreach (var orderDetail in result.items)
                {
                    orderDetail.product_name = (await _productService.GetById(new ItemModel(orderDetail.product_id))).data.product_name;
                    orderDetail.product_price = (await _productService.GetById(new ItemModel(orderDetail.product_id))).data.price;
                }

                return new ResponseService<ListResult<OrderDetailModel>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<OrderDetailModel>>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
            }
        }

    }
}