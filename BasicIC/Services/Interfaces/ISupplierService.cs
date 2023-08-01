using BasicIC.Models.Main.M03;
using Repository.EF;
using Settings.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicIC.Services.Interfaces
{
    public interface ISupplierService : IBaseCRUDService<SupplierModel, M03_Supplier>
    {

    }
}