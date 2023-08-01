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
using Repository.CustomModel;

namespace BasicIC.Services.Implement
{
    public class CartService : BaseCRUDService<CartModel, M03_Cart>, ICartService
    {
        ICartDetailService _cartDetailService;
        public CartService(BasicICRepository<M03_Cart> repo,
            ICartDetailService cartDetailService,
            ILogger logger, IConfigManager config, IMapper mapper) : base(repo, config, logger, mapper)
        {
            _cartDetailService = cartDetailService;
        }

        public async Task<ResponseService<CartModel>> GetByCustomerID(OrderModel param, M03_BasicEntities dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                // Get result from Entity
                var resultEntity = await _repo.FindAsyncWithField("customer_id", param.customer_id, dbContext);
                var data = resultEntity.items[0];

                // Map result to View
                CartModel item;
                try
                {
                    item = _mapper.Map<M03_Cart, CartModel>(data);
                }
                catch
                {
                    return new ResponseService<CartModel>("Error mapping models").BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
                }


                return new ResponseService<CartModel>(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<CartModel>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
            }
        }

        public async Task<ResponseService<CartDetailModel>> AddItemToCart(CartDetailModel param, M03_BasicEntities dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                param.AddInfo();
                var result = await _cartDetailService.Create(param);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<CartDetailModel>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
            }
        }
    }
}