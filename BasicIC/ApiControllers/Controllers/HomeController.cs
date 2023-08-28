using BasicIC.Models.Main;
using BasicIC.Services.Interfaces;
using Common.Commons;
using Settings.Common;
using System.Threading.Tasks;
using System.Web.Http;

namespace BasicIC.ApiControllers.Controllers
{
    [RoutePrefix("api/home")]
    public class HomeController : ApiController
    {
        private readonly IAuthenticationService _authenticationFunction;
        public HomeController(IAuthenticationService authenticationFunction)
        {
            _authenticationFunction = authenticationFunction;
        }

        [Route("confirm-account")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll(ConfirmAccountRequest param)
        {
            ResponseService<string> response = await _authenticationFunction.ConfirmAccount(param);
            if (response.status)
                return Ok(response);

            return new ResponseFail<string>().Error(response);
        }
    }
}