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
    [RoutePrefix("api/attribute")]
    public class AttributeController : ApiController
    {
        private readonly IAttributeService _attributeService;

        public AttributeController(IAttributeService atrributeService)
        {
            _attributeService = atrributeService;
        }

        [Route("get-all")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll(PagingParam param)
        {
            ResponseService<ListResult<AttributeModel>> response = await _attributeService.GetAll(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<ListResult<AttributeModel>>().Error(response);
        }

        [Route("create")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> Add(AttributeModel param)
        {
            ResponseService<AttributeModel> response = await _attributeService.Create(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<AttributeModel>().Error(response);
        }

        //[Route("update")]
        //[ValidateModel]
        //[HttpPost]
        //public async Task<IHttpActionResult> Update(CustomerModel param)
        //{
        //    ResponseService<CustomerModel> response = (await _customerService.Update(param)).Item1;
        //    if (response.status)
        //        return Ok(response);
        //    return new ResponseFail<CustomerModel>().Error(response);
        //}


        //[Route("delete")]
        //[ValidateModel]
        //[HttpPost]
        //public async Task<IHttpActionResult> Remove([FromBody] ItemModel param)
        //{
        //    ResponseService<bool> response = await _customerService.Delete(param);
        //    if (response.status)
        //        return Ok(response);

        //    return new ResponseFail<bool>().Error(response);
        //}

        //[Route("get-item")]
        //[ValidateModel]
        //[HttpPost]
        //public async Task<IHttpActionResult> GetById([FromBody] ItemModel param)
        //{
        //    ResponseService<CustomerModel> response = await _customerService.GetById(param);
        //    if (response.status)
        //        return Ok(response);

        //    return new ResponseFail<CustomerModel>().Error(response);
        //}
    }
}
