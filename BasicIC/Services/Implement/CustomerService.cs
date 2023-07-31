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
        public CustomerService(BasicICRepository<M03_Customer> repo,
             ILogger logger, IConfigManager config, IMapper mapper) : base(repo, config, logger, mapper)
        {
        }

        //public async Task<ResponseService<CustomerModel>> Create(CustomerModel param, M03_BasicEntities dbContext = null)
        //{
        //    try
        //    {
        //        _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

        //        param.AddInfo();
        //        M03_Customer vData;
        //        try
        //        {
        //            vData = _mapper.Map<CustomerModel, M03_Customer>(param);
        //        }
        //        catch (Exception)
        //        {
        //            return new ResponseService<CustomerModel>("Error mapping models").BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
        //        }
        //        M03_Customer result = await _repo.Create(vData, dbContext);

        //        // Map back to CustomerModel
        //        CustomerModel customerModel;
        //        try
        //        {
        //            customerModel = _mapper.Map<M03_Customer, CustomerModel>(result);
        //        }
        //        catch (Exception)
        //        {
        //            return new ResponseService<CustomerModel>("Error mapping models").BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
        //        }

        //        return new ResponseService<CustomerModel>(customerModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex);
        //        return new ResponseService<CustomerModel>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
        //    }
        //}
    }
}