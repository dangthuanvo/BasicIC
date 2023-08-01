using BasicIC.Models.Main.M03;
using Common.Commons;
using Repository.EF;
using Settings.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BasicIC.Services.Interfaces
{
    public interface ICustomerService : IBaseCRUDService <CustomerModel, M03_Customer>
    {
        Task<ResponseService<bool>> DeleteRelatives(CustomerModel param, M03_BasicEntities dbContext = null);
    }
}