using BasicIC.Models.Main.M03;
using Repository.EF;
using Settings.Services.Interfaces;

namespace BasicIC.Services.Interfaces
{
    public interface IAttributeService : IBaseCRUDService<AttributeModel, M03_Attribute>
    {
    }
}