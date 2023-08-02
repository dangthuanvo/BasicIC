using BasicIC.Models.Main.M03;
using Common.Commons;
using Repository.CustomModel;
using Repository.EF;
using Settings.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BasicIC.Services.Interfaces
{
    public interface IOrderService : IBaseCRUDService<OrderModel, M03_Order>
    {
        Task<ResponseService<OrderModel>> CreateFromCart(OrderModel param1, M03_BasicEntities dbContext = null);
        Task<ResponseService<OrderMasterModel>> UpdateMaster(OrderMasterModel obj, M03_BasicEntities dbContext = null);
        Task<ResponseService<OrderMasterModel>> GetMaster(OrderModel obj, M03_BasicEntities dbContext = null);
        Task<ResponseService<bool>> DeleteRelatives(OrderModel param, M03_BasicEntities dbContext = null);
        Task<ResponseService<ListResult<OrderModel>>> GetByCustomerID(CustomerModel param, M03_BasicEntities dbContext = null);
        Task<ResponseService<ListResult<OrderMasterModel>>> GetAllByCustomer(CustomerModel param, M03_BasicEntities dbContext = null);
    }
}