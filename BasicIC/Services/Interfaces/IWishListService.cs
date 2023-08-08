using BasicIC.Models.Main.M03;
using Common.Commons;
using Repository.EF;
using Settings.Services.Interfaces;
using System.Threading.Tasks;

namespace BasicIC.Services.Interfaces
{
    public interface IWishListService : IBaseCRUDService<WishListModel, M03_WishList>
    {
        Task<ResponseService<bool>> DeleteByCustomer(CustomerModel param, M03_BasicEntities dbContext = null);

    }
}