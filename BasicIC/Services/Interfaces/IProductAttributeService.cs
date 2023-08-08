using BasicIC.Models.Main.M03;
using Common.Commons;
using Repository.CustomModel;
using Repository.EF;
using Settings.Services.Interfaces;
using System.Threading.Tasks;

namespace BasicIC.Services.Interfaces
{
    public interface IProductAttributeService: IBaseCRUDService<ProductAttributeModel, M03_ProductAttribute>
    {
        Task<ResponseService<ProductAttributeModel>> UpdateValueOnly(ProductAttributeModel param, M03_BasicEntities dbContext = null);
        Task<ResponseService<ListResult<ProductAttributeModel>>> GetByProductID(ProductModel param, M03_BasicEntities dbContext = null);
        Task<ResponseService<bool>> DeleteByProduct(ProductModel param, M03_BasicEntities dbContext = null);
    }
}
