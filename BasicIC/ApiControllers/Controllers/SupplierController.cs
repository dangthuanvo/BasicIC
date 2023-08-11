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

namespace BasicIC.ApiControllers.Controllers
{
    //[Authorized]
    [RoutePrefix("api/supplier")]
    public class SupplierController : ApiController
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [Route("get-all")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll(PagingParam param)
        {
            ResponseService<ListResult<SupplierModel>> response = await _supplierService.GetAll(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<ListResult<SupplierModel>>().Error(response);
        }

        [Route("create")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> Add(SupplierModel param)
        {
            ResponseService<SupplierModel> response = await _supplierService.Create(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<SupplierModel>().Error(response);
        }

        [Route("update")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> Update(SupplierModel param)
        {
            ResponseService<SupplierModel> response = (await _supplierService.Update(param)).Item1;
            if (response.status)
                return Ok(response);
            return new ResponseFail<SupplierModel>().Error(response);
        }


        [Route("delete")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> Remove(ItemModel param)
        {
            ResponseService<bool> response = await _supplierService.Delete(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<bool>().Error(response);
        }

        [Route("get-item")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> GetById(ItemModel param)
        {
            ResponseService<SupplierModel> response = await _supplierService.GetById(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<SupplierModel>().Error(response);
        }
    }
}
