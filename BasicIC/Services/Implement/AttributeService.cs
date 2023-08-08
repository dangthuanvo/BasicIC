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
using System.Threading.Tasks;


namespace BasicIC.Services.Implement
{
    public class AttributeService : BaseCRUDService<AttributeModel, M03_Attribute>, IAttributeService
    {
        public AttributeService(BasicICRepository<M03_Attribute> repo,
             ILogger logger, IConfigManager config, IMapper mapper) : base(repo, config, logger, mapper)
        {
        }

        public async Task<ResponseService<AttributeModel>> Create(AttributeModel param, M03_BasicEntities dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                param.AddInfo();
                M03_Attribute vData;
                try
                {
                    vData = _mapper.Map<AttributeModel, M03_Attribute>(param);
                }
                catch (Exception)
                {
                    return new ResponseService<AttributeModel>(Constants.ERROR_MAPPING_MODEL).BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
                }
                M03_Attribute result = await _repo.Create(vData, dbContext);

                AttributeModel attributeModel;
                try
                {
                    attributeModel = _mapper.Map<M03_Attribute, AttributeModel>(result);
                }
                catch (Exception)
                {
                    return new ResponseService<AttributeModel>(Constants.ERROR_MAPPING_MODEL).BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
                }

                return new ResponseService<AttributeModel>(attributeModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<AttributeModel>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
            }
        }
    }
}