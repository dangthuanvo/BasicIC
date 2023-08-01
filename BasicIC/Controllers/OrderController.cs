using BasicIC.CustomAttributes;
using BasicIC.Models.Common;
using Common.Commons;
using Common.Params.Base;
using Repository.CustomModel;
using BasicIC.Common;
using BasicIC.Models.Main.M03;
using BasicIC.Services.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;
using Settings.Common;
using System.Web.Http.Results;
using System.Net;

namespace BasicIC.Controllers
{
    //[Authorized]
    [RoutePrefix("api/order")]
    public class OrderController : ApiController
    {
        private readonly IOrderService _orderService;
        private readonly IEmployeeService _employeeService;
        private readonly IAddressService _addressService;

        public OrderController(IOrderService orderService, IEmployeeService employeeService, IAddressService addressService)
        {
            _orderService = orderService;
            _employeeService = employeeService;
            _addressService = addressService;
        }

        //[Route("create-emp")]
        //[ValidateModel]
        //[HttpPost]
        //public async Task<IHttpActionResult> AddEmp([FromBody] EmployeeModel param)
        //{
        //    ResponseService<EmployeeModel> response = await _employeeService.Create(param);
        //    if (response.status)
        //        return Ok(response);

        //    return new ResponseFail<EmployeeModel>().Error(response);
        //}

        //[Route("create-add")]
        //[ValidateModel]
        //[HttpPost]
        //public async Task<IHttpActionResult> AddAdd([FromBody] AddressModel param)
        //{
        //    ResponseService<AddressModel> response = await _addressService.Create(param);
        //    if (response.status)
        //        return Ok(response);

        //    return new ResponseFail<AddressModel>().Error(response);
        //}

        [Route("get-all")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll(PagingParam param)
        {
            ResponseService<ListResult<OrderModel>> response = await _orderService.GetAll(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<ListResult<OrderModel>>().Error(response);
        }

        [Route("create")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> Add([FromBody] OrderModel param)
        {
            ResponseService<OrderModel> response = await _orderService.Create(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<OrderModel>().Error(response);
        }

        [Route("create-from-cart")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> AddFromCart([FromBody] OrderModel param1)
        {
            ResponseService<OrderModel> response = await _orderService.CreateFromCart(param1);
            if (response.status)
                return Ok(response);

            return new ResponseFail<OrderModel>().Error(response);
        }

        [Route("update")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> Update([FromBody] OrderMasterModel param1)
        {
            ResponseService<OrderMasterModel> response = await _orderService.UpdateMaster(param1);
            if (response.status)    
                return Ok(response);

            return new ResponseFail<OrderMasterModel>().Error(response);
        }

    }
}
