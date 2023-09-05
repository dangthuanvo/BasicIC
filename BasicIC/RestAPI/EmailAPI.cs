using BasicIC.Models.Main;
using BasicIC.Services.Implement;
using Common;
using Common.ApiHelper;
using Common.Params.Base;
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
        }

        public async Task<bool> SendMail(SendMailModel request)
        {
            PagingParam paging = new PagingParam();
            var _response = await _restfulApi.client.PostAsJsonAsync(ConstantsPath.PATH_PRE_API_SEND_EMAIL, request);
            //var result = await _response.Content.ReadAsAsync<ResponseService<ListResult<M02_DefaultCommonSetting>>>();
            if (_response.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}
