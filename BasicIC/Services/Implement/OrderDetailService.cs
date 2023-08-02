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
    public class OrderDetailService : BaseCRUDService<OrderDetailModel, M03_OrderDetail>, IOrderDetailService
    {
        protected ICartService _cartService;
        public OrderDetailService(BasicICRepository<M03_OrderDetail> repo,
            ICartService cartService,
            ILogger logger, IConfigManager config, IMapper mapper) : base(repo, config, logger, mapper)
        {
            _cartService = cartService;
        }

        public async Task<ResponseService<bool>> DeleteByOrder(OrderModel param, M03_BasicEntities dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                List<M03_OrderDetail> results = await _repo.DeleteAsyncWithField("order_id", param.id, dbContext);

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
                ListResult<M03_OrderDetail> resultEntity = await _repo.FindAsyncWithField("order_id", param.id, dbContext);

                // Map result to View
                List<OrderDetailModel> items;
                try
                {
                    items = _mapper.Map<List<M03_OrderDetail>, List<OrderDetailModel>>(resultEntity.items);
                }
                catch
                {
                    return new ResponseService<ListResult<OrderDetailModel>>("Error mapping models").BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
                }

                ListResult<OrderDetailModel> result = new ListResult<OrderDetailModel>(items, resultEntity.total);

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