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
    public interface ICartDetailService : IBaseCRUDService<CartDetailModel, M03_CartDetail>
    {
        Task<ResponseService<ListResult<CartDetailModel>>> GetByCartID(CartModel param, M03_BasicEntities dbContext = null);
    }
}