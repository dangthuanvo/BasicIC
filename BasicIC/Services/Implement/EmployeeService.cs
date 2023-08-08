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
using Common.Params.Base;

namespace BasicIC.Services.Implement
{
    public class EmployeeService : BaseCRUDService<EmployeeModel, M03_Employee>, IEmployeeService
    {
        public EmployeeService(BasicICRepository<M03_Employee> repo,
            ILogger logger, IConfigManager config, IMapper mapper) : base(repo, config, logger, mapper)
        {
        }
    }
}