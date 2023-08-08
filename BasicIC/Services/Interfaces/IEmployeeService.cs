using BasicIC.Models.Main.M03;
using Repository.EF;
using Settings.Services.Interfaces;

namespace BasicIC.Services.Interfaces
{
    public interface IEmployeeService : IBaseCRUDService<EmployeeModel, M03_Employee>
    {

    }
}