using BasicIC.CustomAttributes;
using BasicIC.Models.Common;
using BasicIC.Models.Main.M03;
using BasicIC.Services.Interfaces;
using Common.Commons;
using Common.Params.Base;
using Repository.CustomModel;
using Settings.Common;
using System.Threading.Tasks;
using System.Web.Http;

namespace BasicIC.Controllers
{
    //[Authorized]
    [RoutePrefix("api/wishlist")]
    public class WishListController : ApiController
    {
        private readonly IWishListService _wishListService;

        public WishListController(IWishListService wishListService)
        {
            _wishListService = wishListService;
        }

        [Route("get-all")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll(PagingParam param)
        {
            ResponseService<ListResult<WishListModel>> response = await _wishListService.GetAll(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<ListResult<WishListModel>>().Error(response);
        }

        [Route("create")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> Add(WishListModel param)
        {
            ResponseService<WishListModel> response = await _wishListService.Create(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<WishListModel>().Error(response);
        }

        [Route("update")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> Update(WishListModel param)
        {
            ResponseService<WishListModel> response = (await _wishListService.Update(param)).Item1;
            if (response.status)
                return Ok(response);
            return new ResponseFail<WishListModel>().Error(response);
        }


        [Route("delete")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> Remove([FromBody] ItemModel param)
        {
            ResponseService<bool> response = await _wishListService.Delete(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<bool>().Error(response);
        }

        [Route("get-item")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> GetById(ItemModel param)
        {
            ResponseService<WishListModel> response = await _wishListService.GetById(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<WishListModel>().Error(response);
        }
    }
}
