using BasicIC.Models.Main.M03;
using Repository.EF;
using Settings.Services.Interfaces;

namespace BasicIC.Services.Interfaces
{
    public interface IAddressService : IBaseCRUDService<AddressModel, M03_Address>
    {
    }
}