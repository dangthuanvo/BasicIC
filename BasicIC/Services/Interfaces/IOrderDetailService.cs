using BasicIC.Models.Main.M03;
using Common.Commons;
using Repository.CustomModel;
using Repository.EF;
using Settings.Services.Interfaces;
using System.Threading.Tasks;

namespace BasicIC.Services.Interfaces
{
    public interface IOrderDetailService : IBaseCRUDService<OrderDetailModel, M03_OrderDetail>
    {
        Task<ResponseService<bool>> DeleteByOrder(OrderModel param, M03_BasicEntities dbContext);
        Task<ResponseService<ListResult<OrderDetailModel>>> GetByOrderID(OrderModel param, M03_BasicEntities dbContext = null);

    }
}