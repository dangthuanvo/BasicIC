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
using BasicIC.Services.Implement;

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
        public async Task<IHttpActionResult> Add(OrderModel param)
        {
            ResponseService<OrderModel> response = await _orderService.Create(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<OrderModel>().Error(response);
        }

        [Route("create-from-cart")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> AddFromCart(OrderModel param1)
        {
            ResponseService<OrderModel> response = await _orderService.CreateFromCart(param1);
            if (response.status)
                return Ok(response);

            return new ResponseFail<OrderModel>().Error(response);
        }

        [Route("update")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> Update(OrderMasterModel param1)
        {
            ResponseService<OrderMasterModel> response = await _orderService.UpdateMaster(param1);
            if (response.status)    
                return Ok(response);

            return new ResponseFail<OrderMasterModel>().Error(response);
        }

        [Route("get-order-detail-by-order-id")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> GetMaster(OrderModel param)
        {
            ResponseService<OrderMasterModel> response = await _orderService.GetMaster(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<OrderMasterModel>().Error(response);
        }

        [Route("get-add-order-detail-by-customer-id")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> GetAllByCustomer(CustomerModel param)
        {
            ResponseService<ListResult<OrderMasterModel>> response = await _orderService.GetAllByCustomer(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<ListResult<OrderMasterModel>>().Error(response);
        }

        [Route("delete")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> Remove(ItemModel param)
        {
            ResponseService<bool> response = await _orderService.Delete(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<bool>().Error(response);
        }

        [Route("delete_relatives")]
        [ValidateModel]
        [HttpPost]
        public async Task<IHttpActionResult> RemoveRelatives(OrderModel param)
        {
            ResponseService<bool> response = await _orderService.DeleteRelatives(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<bool>().Error(response);
        }
    }
}
