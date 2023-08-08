using BasicIC.Models.Main.M03;
using Common.Commons;
using Repository.CustomModel;
using Repository.EF;
using Settings.Services.Interfaces;
using System.Threading.Tasks;

namespace BasicIC.Services.Interfaces
{
    public interface ICartDetailService : IBaseCRUDService<CartDetailModel, M03_CartDetail>
    {
        Task<ResponseService<ListResult<CartDetailModel>>> GetByCartID(CartModel param, M03_BasicEntities dbContext = null);
    }


}