using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SystemManagement.Common;
using SystemManagement.Dto;
using SystemManagement.Service.Contract;

namespace SystemManagement.Controllers
{
    [Route("api/[controller]")]
    [Permission("log")]
    public class LogController : Controller
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet("operationLog/list")]
        public async Task<PagedModel<SysOperationLogDto>> SearchOperationLogs(LogSearchModel searchModel)
        {
            return await _logService.SearchOperationLogs(searchModel);
        }

        [HttpDelete("operationLog")]
        [Permission("logClear")]
        public async Task ClearOperationLogs()
        {
            await _logService.ClearOperationLogs();
        }
    }
}
