﻿using BasicIC.Models.Main.M03;
using Common.Commons;
using Repository.CustomModel;
using Repository.EF;
using Settings.Services.Interfaces;
using System.Threading.Tasks;

namespace BasicIC.Services.Interfaces
{
    public interface IOrderService : IBaseCRUDService<OrderModel, M03_Order>
    {
        Task<ResponseService<OrderModel>> CreateFromCart(OrderModel param1, M03_BasicEntities dbContext = null);
        Task<ResponseService<OrderOrderDetailModel>> UpdateOrderOrderDetail(OrderOrderDetailModel obj, M03_BasicEntities dbContext = null);
        Task<ResponseService<OrderOrderDetailModel>> GetOrderOrderDetail(OrderModel obj, M03_BasicEntities dbContext = null);
        Task<ResponseService<bool>> DeleteRelatives(OrderModel param, M03_BasicEntities dbContext = null);
        Task<ResponseService<ListResult<OrderModel>>> GetByCustomerID(CustomerModel param, M03_BasicEntities dbContext = null);
        Task<ResponseService<ListResult<OrderOrderDetailModel>>> GetAllByCustomer(CustomerModel param, M03_BasicEntities dbContext = null);
    }
}