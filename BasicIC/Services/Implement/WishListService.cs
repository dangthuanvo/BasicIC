using AutoMapper;
using BasicIC.Interfaces;
using BasicIC.Models.Main.M03;
using BasicIC.Services.Interfaces;
using Common;
using Common.Commons;
using Common.Interfaces;
using Repository.EF;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicIC.Services.Implement
{
    public class WishListService : BaseCRUDService<WishListModel, M03_WishList>, IWishListService
    {
        public WishListService(BasicICRepository<M03_WishList> repo,
            ILogger logger, IConfigManager config, IMapper mapper) : base(repo, config, logger, mapper)
        {
        }

        public async Task<ResponseService<bool>> DeleteByCustomer(CustomerModel param, M03_BasicEntities dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                List<M03_WishList> results = await _repo.DeleteAsyncWithField(nameof(WishListModel.customer_id), param.id, dbContext);

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
    }
}