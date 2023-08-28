using BasicIC.Models.Main;
using BasicIC.Services.Implement;
using Common;
using Common.ApiHelper;
using System.Net.Http;
using System.Threading.Tasks;

namespace BasicIC.RestApi
{
    public class EmailAPI : BaseService
    {
        private readonly RestfulApi _restfulApi;

        public EmailAPI()
        {
            _restfulApi = new RestfulApi().ToFabio(ConstantsPath.source_mail, "");
            _restfulApi.client.DefaultRequestHeaders.Add("API_SECRET_KEY", ConstantsPath.seret_key);
        }

        public async Task<bool> SendMail(SendMailModel request)
        {
            var _response = await _restfulApi.client.PostAsJsonAsync(ConstantsPath.PATH_PRE_API_SEND_EMAIL, request);
            if (_response.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}
