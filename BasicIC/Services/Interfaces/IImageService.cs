using BasicIC.Models.Common;
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
    public interface IImageService : IBaseCRUDService<ImageModel, M03_Image>
    {
        Task<ResponseService<ListResult<ImageModel>>> GetByProductID(ImageModel param, M03_BasicEntities dbContext = null);
        Task<ResponseService<bool>> DeleteByProduct(ProductModel param, M03_BasicEntities dbContext = null);
    }
}