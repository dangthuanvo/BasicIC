using AutoMapper;
using BasicIC.Common;
using Common;
using Common.Interfaces;
using Common.Models;
using BasicIC.Common;
using BasicIC.Interfaces;
using System;
using System.Diagnostics;

namespace BasicIC.Services.Implement
{
    public class BaseService
    {
        protected readonly IConfigManager _config;
        protected readonly ILogger _logger;
        protected readonly IMapper _mapper;

        protected BaseService(IConfigManager config, ILogger logger, IMapper mapper)
        {
            _config = config;
            _logger = logger;
            _mapper = mapper;
        }

        public static string GetMethodName(StackTrace stackTrace)
        {
            var method = stackTrace.GetFrame(0).GetMethod();

            string _methodName = method.DeclaringType.FullName;

            if (_methodName.Contains(">") || _methodName.Contains("<"))
            {
                _methodName = _methodName.Split('<', '>')[1];
            }
            else
            {
                _methodName = method.Name;
            }

            return _methodName;
        }
    }
}