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
    public class ProductAttributeService : BaseCRUDService<ProductAttributeModel, M03_ProductAttribute>, IProductAttributeService
    {
        public ProductAttributeService(BasicICRepository<M03_ProductAttribute> repo,
            ILogger logger, IConfigManager config, IMapper mapper) : base(repo, config, logger, mapper)
        {
        }

        public async Task<ResponseService<ProductAttributeModel>> Create(ProductAttributeModel param, M03_BasicEntities dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                param.AddInfo();
                M03_ProductAttribute vData;
                try
                {
                    vData = _mapper.Map<ProductAttributeModel, M03_ProductAttribute>(param);
                }
                catch (Exception)
                {
                    return new ResponseService<ProductAttributeModel>(Constants.ERROR_MAPPING_MODEL).BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
                }

                if (vData.M03_Product == null)
                {
                    return new ResponseService<ProductAttributeModel>(Constants.RECORD_NOT_FOUND).BadRequest(ErrorCodes.RECORD_NOT_FOUND);
                }
                if (vData.M03_Attribute == null)
                {
                    return new ResponseService<ProductAttributeModel>(Constants.RECORD_NOT_FOUND).BadRequest(ErrorCodes.RECORD_NOT_FOUND);
                }
                M03_ProductAttribute result = await _repo.Create(vData, dbContext);


                ProductAttributeModel productAttributeModel;
                try
                {
                    productAttributeModel = _mapper.Map<M03_ProductAttribute, ProductAttributeModel>(result);
                }
                catch (Exception)
                {
                    return new ResponseService<ProductAttributeModel>(Constants.ERROR_MAPPING_MODEL).BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
                }

                return new ResponseService<ProductAttributeModel>(productAttributeModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ProductAttributeModel>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
            }
        }

        public async Task<ResponseService<ListResult<ProductAttributeModel>>> GetByProductID(ProductModel param, M03_BasicEntities dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                // Get result from Entity
                ListResult<M03_ProductAttribute> resultEntity = await _repo.FindAsyncWithField(nameof(ProductAttributeModel.product_id), param.id, dbContext);

                // Map result to View
                List<ProductAttributeModel> items;
                try
                {
                    items = _mapper.Map<List<M03_ProductAttribute>, List<ProductAttributeModel>>(resultEntity.items);
                }
                catch
                {
                    return new ResponseService<ListResult<ProductAttributeModel>>(Constants.ERROR_MAPPING_MODEL).BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
                }

                ListResult<ProductAttributeModel> result = new ListResult<ProductAttributeModel>(items, resultEntity.total);

                return new ResponseService<ListResult<ProductAttributeModel>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<ProductAttributeModel>>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
            }
        }

        public async Task<ResponseService<bool>> DeleteByProduct(ProductModel param, M03_BasicEntities dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                List<M03_ProductAttribute> results = await _repo.DeleteAsyncWithField(nameof(ProductAttributeModel.product_id), param.id, dbContext);

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

        public async Task<ResponseService<ProductAttributeModel>> UpdateValueOnly(ProductAttributeModel param, M03_BasicEntities dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                M03_ProductAttribute resultDb = await _repo.GetById(param.id, dbContext);

                if (resultDb == null)
                {
                    return new ResponseService<ProductAttributeModel>(Constants.ERROR_MAPPING_MODEL).BadRequest(ErrorCodes.RECORD_NOT_FOUND);
                }

                ProductAttributeModel TResultDb;
                try
                {
                    TResultDb = _mapper.Map<M03_ProductAttribute, ProductAttributeModel>(resultDb);
                }
                catch
                {
                    return new ResponseService<ProductAttributeModel>(Constants.ERROR_MAPPING_MODEL).BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
                }
                param.UpdateInfo(TResultDb);

                // Map new updated T to V then update V on db
                M03_ProductAttribute vData;
                try
                {
                    vData = _mapper.Map(param, resultDb);
                }
                catch
                {
                    return new ResponseService<ProductAttributeModel>(Constants.ERROR_MAPPING_MODEL).BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
                }
                List<string> fields = new List<string>
                {
                    nameof(ProductAttributeModel.attribute_value)
                };

                M03_ProductAttribute result = await _repo.UpdateFields(vData, fields, dbContext);

                return new ResponseService<ProductAttributeModel>(_mapper.Map<M03_ProductAttribute, ProductAttributeModel>(result));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ProductAttributeModel>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
            }
        }
    }
}