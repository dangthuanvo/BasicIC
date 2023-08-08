using AutoMapper;
using BasicIC.Interfaces;
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
    public class CartDetailService : BaseCRUDService<CartDetailModel, M03_CartDetail>, ICartDetailService
    {
        private readonly BasicICRepository<M03_Cart> _repoCart;
        public CartDetailService(BasicICRepository<M03_CartDetail> repo,
            BasicICRepository<M03_Cart> repoCart,
            ILogger logger, IConfigManager config, IMapper mapper) : base(repo, config, logger, mapper)
        {
            _repoCart = repoCart;
        }

        public async Task<ResponseService<ListResult<CartDetailModel>>> GetByCartID(CartModel param, M03_BasicEntities dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                // Get result from Entity
                ListResult<M03_CartDetail> resultEntity = await _repo.FindAsyncWithField(nameof(CartDetailModel.cart_id), param.id, dbContext);

                // Map result to View
                List<CartDetailModel> items;
                try
                {
                    items = _mapper.Map<List<M03_CartDetail>, List<CartDetailModel>>(resultEntity.items);
                }
                catch
                {
                    return new ResponseService<ListResult<CartDetailModel>>(Constants.ERROR_MAPPING_MODEL).BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
                }

                ListResult<CartDetailModel> result = new ListResult<CartDetailModel>(items, resultEntity.total);

                return new ResponseService<ListResult<CartDetailModel>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<CartDetailModel>>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
            }
        }
    }
}