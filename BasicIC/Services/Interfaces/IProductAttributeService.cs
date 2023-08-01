using BasicIC.Models.Main.M03;
using Common.Commons;
using Repository.CustomModel;
using Repository.EF;
using Settings.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BasicIC.Services.Interfaces
{
    public interface IProductAttributeService: IBaseCRUDService<ProductAttributeModel, M03_ProductAttribute>
    {
        Task<ResponseService<ProductAttributeModel>> UpdateValueOnly(ProductAttributeModel param, M03_BasicEntities dbContext = null);
        Task<ResponseService<ListResult<ProductAttributeModel>>> GetByProductID(ProductAttributeModel param, M03_BasicEntities dbContext = null);
        Task<ResponseService<bool>> DeleteByProduct(ProductModel param, M03_BasicEntities dbContext = null);
    }
}
