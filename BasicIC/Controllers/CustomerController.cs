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
    [RoutePrefix("api/customer")]
    public class CustomerController : ApiController
    {
        private readonly ICustomerService _customerService;
        
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        //public CustomerController() : base() { }
        /// <summary>
        /// Type: API method
        /// Description: API for client to get map agent group (all)
        /// Request point: Client
        /// Authen: Yes, Bearer Token
        /// API document: https://docs.google.com/document/d/1xAZ6fG8ApHsajBu_ekNdcEghomp-ezqVqw3r13dlWXA/
        /// Owner: trint
        /// Create log:     15.11.2022 - trint        
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        //[PermissionAttributeFilter("Map Agent Group", "access")]
        //[Route("get-all")]
        //[HttpPost]
        //public async Task<IHttpActionResult> GetAll(PagingParam param)
        //{
        //    ResponseService<ListResult<MapAgentGroupModel>> response = await _mapAgentGroupService.GetAll(param);
        //    if (response.status)
        //        return Ok(response);

        //    return new ResponseFail<ListResult<MapAgentGroupModel>>().Error(response);
        //}

        /// <summary>
        /// Type: API method
        /// Description: API for client to create map agent group
        /// Request point: Client
        /// Authen: Yes, Bearer Token
        /// API document: https://docs.google.com/document/d/1xAZ6fG8ApHsajBu_ekNdcEghomp-ezqVqw3r13dlWXA/
        /// Owner: trint
        /// Create log:     15.11.2022 - trint        
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [Route("create")]
        //[PermissionAttributeFilter("Map Agent Group", "create")]
        [HttpPost]
        public async Task<IHttpActionResult> Create(CustomerModel param)
        {
            ResponseService<CustomerModel> response = await _customerService.Create(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<CustomerModel>().Error(response);

        }

        /// <summary>
        /// Type: API method
        /// Description: API for client to update map agent group
        /// Request point: Client
        /// Authen: Yes, Bearer Token
        /// API document: https://docs.google.com/document/d/1xAZ6fG8ApHsajBu_ekNdcEghomp-ezqVqw3r13dlWXA/
        /// Owner: trint
        /// Create log:     15.11.2022 - trint        
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        //[Route("update")]
        //[PermissionAttributeFilter("Map Agent Group", "edit")]
        //[ValidateModel]
        //[HttpPost]
        //public async Task<IHttpActionResult> Update(MapAgentGroupModel param)
        //{
        //    ResponseService<MapAgentGroupModel> response = (await _mapAgentGroupService.Update(param)).Item1;
        //    if (response.status)
        //        return Ok(response);

        //    return new ResponseFail<MapAgentGroupModel>().Error(response);
        //}

        ///// <summary>
        ///// Type: API method
        ///// Description: API for client to delete map agent group
        ///// Request point: Client
        ///// Authen: Yes, Bearer Token
        ///// API document: https://docs.google.com/document/d/1xAZ6fG8ApHsajBu_ekNdcEghomp-ezqVqw3r13dlWXA/
        ///// Owner: trint
        ///// Create log:     15.11.2022 - trint        
        ///// </summary>
        ///// <param name="param"></param>
        ///// <returns></returns>
        //[Route("delete")]
        //[PermissionAttributeFilter("Map Agent Group", "delete")]
        //[ValidateModel]
        //[HttpPost]
        //public async Task<IHttpActionResult> Remove([FromBody] ItemModel param)
        //{
        //    ResponseService<bool> response = await _mapAgentGroupService.Delete(param);
        //    if (response.status)
        //        return Ok(response);

        //    return new ResponseFail<bool>().Error(response);
        //}

        ///// <summary>
        ///// Type: API method
        ///// Description: API for client to get map agent group (by id)
        ///// Request point: Client
        ///// Authen: Yes, Bearer Token
        ///// API document: https://docs.google.com/document/d/1xAZ6fG8ApHsajBu_ekNdcEghomp-ezqVqw3r13dlWXA/
        ///// Owner: trint
        ///// Create log:     15.11.2022 - trint        
        ///// </summary>
        ///// <param name="param"></param>
        ///// <returns></returns>
        //[Route("get-item")]
        //[PermissionAttributeFilter("Map Agent Group", "access")]
        //[ValidateModel]
        //[HttpPost]
        //public async Task<IHttpActionResult> GetById([FromBody] ItemModel param)
        //{
        //    ResponseService<MapAgentGroupModel> response = await _mapAgentGroupService.GetById(param);
        //    if (response.status)
        //        return Ok(response);

        //    return new ResponseFail<MapAgentGroupModel>().Error(response);
        //}

        ///// <summary>
        ///// Type: API method
        ///// Description: API for client to get map agent group (all global search)
        ///// Request point: Client
        ///// Authen: Yes, Bearer Token
        ///// API document: https://docs.google.com/document/d/1xAZ6fG8ApHsajBu_ekNdcEghomp-ezqVqw3r13dlWXA/
        ///// Owner: trint
        ///// Create log:     15.11.2022 - trint        
        ///// </summary>
        ///// <param name="param"></param>
        ///// <returns></returns>
        //[Route("get-all-table-search")]
        //[PermissionAttributeFilter("Map Agent Group", "access")]
        //[ValidateModel]
        //[HttpPost]
        //public async Task<IHttpActionResult> GetAllGlobalSearch(PagingParamGlobalSearch param)
        //{
        //    ResponseService<ListResult<MapAgentGroupModel>> response = await _mapAgentGroupService.GetAllGlobalSearch(param);
        //    if (response.status)
        //        return Ok(response);

        //    return new ResponseFail<ListResult<MapAgentGroupModel>>().Error(response);
        //}
    }
}
