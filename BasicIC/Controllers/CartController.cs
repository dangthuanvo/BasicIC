using BasicIC.CustomAttributes;
using BasicIC.Models.Common;
using Common.Commons;
using Common.Params.Base;
using Repository.CustomModel;
using BasicIC.Common;
using BasicIC.CustomAttributes;
using BasicIC.Models.Common;
using BasicIC.Models.Main.M03;
using BasicIC.Services.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;
using Settings.Common;
using System.Web.Http.Results;

namespace BasicIC.Controllers
{
    //[Authorized]
    [RoutePrefix("api/cart")]
    public class CartController : ApiController
    {
        private readonly ICartService _cartService ;
        private readonly ICartDetailService _cartDetailService;

        public CartController(ICartService cartService, ICartDetailService cartDetailService)
        {
            _cartService = cartService;
            _cartDetailService = cartDetailService;
        }

        [Route("get-all")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll(PagingParam param)
        {
            ResponseService<ListResult<CartModel>> response = await _cartService.GetAll(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<ListResult<CartModel>>().Error(response);
        }

        [Route("get-by-cart")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> GetByCart(CartModel param)
        {
            ResponseService<ListResult<CartDetailModel>> response = await _cartDetailService.GetByCartID(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<ListResult<CartDetailModel>>().Error(response);
        }

        [Route("create")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> Add(CartModel param)
        {
            ResponseService<CartModel> response = await _cartService.Create(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<CartModel>().Error(response);
        }

        [Route("add-item-to-cart")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> AddItemToCart(CartDetailModel param)
        {
            ResponseService<CartDetailModel> response = await _cartService.AddItemToCart(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<CartDetailModel>().Error(response);
        }

        //[Route("update")]
        //[ValidateModel]
        //[HttpPost]
        //public async Task<IHttpActionResult> Update(WishListModel param)
        //{
        //    ResponseService<WishListModel> response = (await _wishListService.Update(param)).Item1;
        //    if (response.status)
        //        return Ok(response);
        //    return new ResponseFail<WishListModel>().Error(response);
        //}


        //[Route("delete")]
        //[ValidateModel]
        //[HttpPost]
        //public async Task<IHttpActionResult> Remove([FromBody] ItemModel param)
        //{
        //    ResponseService<bool> response = await _wishListService.Delete(param);
        //    if (response.status)
        //        return Ok(response);

        //    return new ResponseFail<bool>().Error(response);
        //}

        //[Route("get-item")]
        //[ValidateModel]
        //[HttpPost]
        //public async Task<IHttpActionResult> GetById([FromBody] ItemModel param)
        //{
        //    ResponseService<WishListModel> response = await _wishListService.GetById(param);
        //    if (response.status)
        //        return Ok(response);

        //    return new ResponseFail<WishListModel>().Error(response);
        //}
    }
}
