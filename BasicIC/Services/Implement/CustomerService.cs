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

namespace BasicIC.Services.Implement
{
    public class CustomerService : BaseCRUDService<CustomerModel, M03_Customer>, ICustomerService
    {
        private readonly IWishListService _wishListService;

        public CustomerService(BasicICRepository<M03_Customer> repo,
            IWishListService wishListService,
          
            ILogger logger, IConfigManager config, IMapper mapper) : base(repo, config, logger, mapper)
        {
            _wishListService= wishListService;
        }

        public async Task<ResponseService<bool>> DeleteRelatives(CustomerModel param, M03_BasicEntities dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                ResponseService<bool> a = await _wishListService.DeleteByCustomer(param, dbContext);
                M03_Customer result = await _repo.Delete(param.id, dbContext);


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
    }
}