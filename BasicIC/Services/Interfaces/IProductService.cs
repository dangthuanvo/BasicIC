using BasicIC.Models.Main.M03;
using Common.Commons;
using Repository.EF;
using Settings.Services.Interfaces;
using System.Threading.Tasks;

namespace BasicIC.Services.Interfaces
{
    public interface IProductService : IBaseCRUDService<ProductModel, M03_Product>
    {
        Task<ResponseService<bool>> DeleteRelatives(ProductModel param, M03_BasicEntities dbContext = null);
    }
}