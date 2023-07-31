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
    public class ProductService : BaseCRUDService<ProductModel, M03_Product>, IProductService
    {
        public ProductService(BasicICRepository<M03_Product> repo,
            ILogger logger, IConfigManager config, IMapper mapper) : base(repo, config, logger, mapper)
        {
        }
        public async Task<ResponseService<ProductModel>> Create(ProductModel param, M03_BasicEntities dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                param.AddInfo();
                M03_Product vData;
                try
                {
                    vData = _mapper.Map<ProductModel, M03_Product>(param);
                }
                catch (Exception)
                {
                    return new ResponseService<ProductModel>("Error mapping models").BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
                }
                M03_Product result = await _repo.Create(vData, dbContext);


                ProductModel productModel;
                try
                {
                    productModel = _mapper.Map<M03_Product, ProductModel>(result);
                }
                catch (Exception)
                {
                    return new ResponseService<ProductModel>("Error mapping models").BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
                }

                return new ResponseService<ProductModel>(productModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ProductModel>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
            }


        }
    }
}