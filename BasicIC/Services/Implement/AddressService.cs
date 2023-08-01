using AutoMapper;
using BasicIC.Interfaces;
using BasicIC.Models.Main.M03;
using BasicIC.Services.Interfaces;
using Common.Commons;
using Common;
using Common.Interfaces;
using Repository.EF;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

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