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
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;
        private readonly IImageService _imageService;
        private readonly IProductAttributeService _productattributeSerivce;

        public ProductController(IProductService productService, IImageService imageService, IProductAttributeService productattributeService)
        {
            _productService = productService;
            _imageService = imageService;
            _productattributeSerivce = productattributeService;
        }

        [Route("get-all")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll(PagingParam param)
        {
            ResponseService<ListResult<ProductModel>> response = await _productService.GetAll(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<ListResult<ProductModel>>().Error(response);
        }

        [Route("create")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> Add(ProductModel param)
        {
            ResponseService<ProductModel> response = await _productService.Create(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<ProductModel>().Error(response);
        }

        [Route("add-image")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> AddImage(ImageModel param)
        {
            ResponseService<ImageModel> response = await _imageService.Create(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<ImageModel>().Error(response);
        }

        [Route("link-attribute")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> LinkAttribute(ProductAttributeModel param)
        {
            ResponseService<ProductAttributeModel> response = await _productattributeSerivce.Create(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<ProductAttributeModel>().Error(response);
        }

        [Route("update")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> Update(ProductModel param)
        {
            ResponseService<ProductModel> response = (await _productService.Update(param)).Item1;
            if (response.status)
                return Ok(response);
            return new ResponseFail<ProductModel>().Error(response);
        }

        [Route("update-attribute")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateProductAttribute(ProductAttributeModel param)
        {
            ResponseService<ProductAttributeModel> response = (await _productattributeSerivce.UpdateValueOnly(param));
            if (response.status)
                return Ok(response);
            return new ResponseFail<ProductAttributeModel>().Error(response);
        }
        [Route("update-image")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateImage(ImageModel param)
        {
            ResponseService<ImageModel> response = (await _imageService.Update(param)).Item1;
            if (response.status)
                return Ok(response);
            return new ResponseFail<ImageModel>().Error(response);
        }

        [Route("get-product-item")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> GetProductById(ItemModel param)
        {
            ResponseService<ProductModel> response = await _productService.GetById(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<ProductModel>().Error(response);
        }

        [Route("get-attribute-item")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> GetAttributeByProductId(ProductModel param)
        {
            ResponseService<ListResult<ProductAttributeModel>> response = await _productattributeSerivce.GetByProductID(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<ListResult<ProductAttributeModel>>().Error(response);
        }

        [Route("get-image-item")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> GetImageByProductId(ProductModel param)
        {
            ResponseService<ListResult<ImageModel>> response = await _imageService.GetByProductID(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<ListResult<ImageModel>>().Error(response);
        }

        [Route("delete")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> Remove(ItemModel param)
        {
            ResponseService<bool> response = await _productService.Delete(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<bool>().Error(response);
        }

        [Route("delete_relatives")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> RemoveRelatives(ProductModel param)
        {
            ResponseService<bool> response = await _productService.DeleteRelatives(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<bool>().Error(response);
        }

    }
}
