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
    public class CartService : BaseCRUDService<CartModel, M03_Cart>, ICartService
    {
        private readonly ICartDetailService _cartDetailService;
        private readonly BasicICRepository<M03_CartDetail> _repoCartDetail;

        public CartService(BasicICRepository<M03_Cart> repo,
            ICartDetailService cartDetailService,
            BasicICRepository<M03_CartDetail> repoCartDetail,
        ILogger logger, IConfigManager config, IMapper mapper) : base(repo, config, logger, mapper)
        {
            _cartDetailService = cartDetailService;
            _repoCartDetail = repoCartDetail;
        }

        public async Task<ResponseService<CartModel>> GetByCustomerID(OrderModel param, M03_BasicEntities dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                // Get result from Entity

                var resultEntity = await _repo.FindAsyncWithField(nameof(CartModel.customer_id), param.customer_id, dbContext);
                var data = resultEntity.items[0];

                // Map result to View
                CartModel item;
                try
                {
                    item = _mapper.Map<M03_Cart, CartModel>(data);
                }
                catch
                {
                    return new ResponseService<CartModel>(Constants.ERROR_MAPPING_MODEL).BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
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
                CartModel cartModel = _mapper.Map<M03_Cart, CartModel>((await _repo.GetById(param.cart_id)));

                List<CartDetailModel> listCartDetailModel = (await _cartDetailService.GetByCartID(cartModel)).data.items;
                bool flag = true;
                foreach (CartDetailModel item in listCartDetailModel)
                {
                    if (item.product_id == param.product_id)
                    {
                        param.quantity += item.quantity;
                        param.id = item.id;
                        param.cart_total_price += item.cart_total_price;
                        flag = false;
                        break;
                    }
                }

                if (flag)
                {
                    var result = await _cartDetailService.Create(param);
                    return result;
                }
                else
                {
                    List<string> fields = new List<string> { nameof(CartDetailModel.quantity) };
                    var result = await _repoCartDetail.UpdateFields(_mapper.Map<CartDetailModel, M03_CartDetail>(param), fields, dbContext);
                    return new ResponseService<CartDetailModel>(_mapper.Map<M03_CartDetail, CartDetailModel>(result));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<CartDetailModel>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
            }
        }


    }
}