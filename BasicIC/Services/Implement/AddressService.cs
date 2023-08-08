using AutoMapper;
using BasicIC.Interfaces;
using BasicIC.Models.Main.M03;
using BasicIC.Services.Interfaces;
using Common.Interfaces;
using Repository.EF;
using Repository.Repositories;

namespace BasicIC.Services.Implement
{
    public class AddressService : BaseCRUDService<AddressModel, M03_Address>, IAddressService
    {
        public AddressService(BasicICRepository<M03_Address> repo,
             ILogger logger, IConfigManager config, IMapper mapper) : base(repo, config, logger, mapper)
        {

        }
    }
}