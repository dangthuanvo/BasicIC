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
    public class ImageService : BaseCRUDService<ImageModel, M03_Image>, IImageService
    {
        public ImageService(BasicICRepository<M03_Image> repo,
            ILogger logger, IConfigManager config, IMapper mapper) : base(repo, config, logger, mapper)
        {
        }
        public async Task<ResponseService<ImageModel>> Create(ImageModel param, M03_BasicEntities dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                param.AddInfo();
                M03_Image vData;
                try
                {
                    vData = _mapper.Map<ImageModel, M03_Image>(param);
                }
                catch (Exception)
                {
                    return new ResponseService<ImageModel>("Error mapping models").BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
                }

                if(vData.M03_Product == null)
                {
                    return new ResponseService<ImageModel>("Record not found").BadRequest(ErrorCodes.RECORD_NOT_FOUND);
                }
                M03_Image result = await _repo.Create(vData, dbContext);


                ImageModel imageModel;
                try
                {
                    imageModel = _mapper.Map<M03_Image, ImageModel>(result);
                }
                catch (Exception)
                {
                    return new ResponseService<ImageModel>("Error mapping models").BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
                }

                return new ResponseService<ImageModel>(imageModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ImageModel>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
            }

        }

        public async Task<ResponseService<bool>> DeleteByProduct(ProductModel param, M03_BasicEntities dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                List<M03_Image> results = await _repo.DeleteAsyncWithField("product_id", param.id, dbContext);

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
        public async Task<ResponseService<ListResult<ImageModel>>> GetByProductID(ImageModel param, M03_BasicEntities dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                // Get result from Entity
                ListResult<M03_Image> resultEntity = await _repo.FindAsyncWithField("product_id", param.product_id, dbContext);

                // Map result to View
                List<ImageModel> items;
                try
                {
                    items = _mapper.Map<List<M03_Image>, List<ImageModel>>(resultEntity.items);
                }
                catch
                {
                    return new ResponseService<ListResult<ImageModel>>("Error mapping models").BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
                }

                ListResult<ImageModel> result = new ListResult<ImageModel>(items, resultEntity.total);

                return new ResponseService<ListResult<ImageModel>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<ImageModel>>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
            }
        }
    }
}