using BasicIC.Models.Main.M03;
using Common.Commons;
using Repository.CustomModel;
using Repository.EF;
using Settings.Services.Interfaces;
using System.Threading.Tasks;

namespace BasicIC.Services.Interfaces
{
    public interface IImageService : IBaseCRUDService<ImageModel, M03_Image>
    {
        Task<ResponseService<ListResult<ImageModel>>> GetByProductID(ProductModel param, M03_BasicEntities dbContext = null);
        Task<ResponseService<bool>> DeleteByProduct( ProductModel param, M03_BasicEntities dbContext = null);
    }
}