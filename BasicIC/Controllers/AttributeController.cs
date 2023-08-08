using BasicIC.CustomAttributes;
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
    //[Authorize] 
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
    }
}
