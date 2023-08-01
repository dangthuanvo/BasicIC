﻿using AutoMapper;
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
    public class CartDetailService : BaseCRUDService<CartDetailModel, M03_CartDetail>, ICartDetailService
    {
        public CartDetailService(BasicICRepository<M03_CartDetail> repo,
            ILogger logger, IConfigManager config, IMapper mapper) : base(repo, config, logger, mapper)
        {
        }

        public async Task<ResponseService<ListResult<CartDetailModel>>> GetByCartID(CartModel param, M03_BasicEntities dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                // Get result from Entity
                ListResult<M03_CartDetail> resultEntity = await _repo.FindAsyncWithField("cart_id", param.id, dbContext);

                // Map result to View
                List<CartDetailModel> items;
                try
                {
                    items = _mapper.Map<List<M03_CartDetail>, List<CartDetailModel>>(resultEntity.items);
                }
                catch
                {
                    return new ResponseService<ListResult<CartDetailModel>>("Error mapping models").BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
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