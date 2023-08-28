using BasicIC.Models.Main;
using Common.Commons;
using System.Threading.Tasks;

namespace BasicIC.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ResponseService<string>> ConfirmAccount(ConfirmAccountRequest request);
    }
}