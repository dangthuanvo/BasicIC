using AutoMapper;
using BasicIC.Interfaces;
using BasicIC.Models.Main.M03;
using BasicIC.Services.Interfaces;
using Common.Interfaces;
using Repository.EF;
using Repository.Repositories;

namespace BasicIC.Services.Implement
{
    public class SupplierService : BaseCRUDService<SupplierModel, M03_Supplier>, ISupplierService
    {
        public SupplierService(BasicICRepository<M03_Supplier> repo,
            ILogger logger, IConfigManager config, IMapper mapper) : base(repo, config, logger, mapper)
        {
        }

    }
}