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
    public class OrderDetailService : BaseCRUDService<OrderDetailModel, M03_OrderDetail>, IOrderDetailService
    {
        protected ICartService _cartService;
        public OrderDetailService(BasicICRepository<M03_OrderDetail> repo,
            ICartService cartService,
            ILogger logger, IConfigManager config, IMapper mapper) : base(repo, config, logger, mapper)
        {
            _cartService = cartService;
        }

      
    }
}