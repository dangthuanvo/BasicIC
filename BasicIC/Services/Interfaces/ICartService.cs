using BasicIC.Models.Main.M03;
using Common.Commons;
using Repository.EF;
using Settings.Services.Interfaces;
using System.Threading.Tasks;

namespace BasicIC.Services.Interfaces
{
    public interface ICartService : IBaseCRUDService<CartModel, M03_Cart>
    {
        Task<ResponseService<CartDetailModel>> AddItemToCart(CartDetailModel param, M03_BasicEntities dbContext = null);
        Task<ResponseService<CartModel>> GetByCustomerID(OrderModel param, M03_BasicEntities dbContext = null);
    }
}