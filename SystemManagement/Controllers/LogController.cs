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
    public class LogController : Controller
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        /// <summary>
        /// 查询操作日志
        /// </summary>
        /// <param name="searchModel">日志查询条件</param>
        /// <returns></returns>
        [HttpGet("operationLog/list")]
        [Permission("log")]
        public async Task<PagedModel<SysOperationLogDto>> SearchOperationLogs(LogSearchModel searchModel)
        {
            return await _logService.SearchOperationLogs(searchModel);
        }

        /// <summary>
        /// 清空操作日志
        /// </summary>
        /// <returns></returns>
        [HttpDelete("operationLog")]
        [Permission("logClear")]
        public async Task ClearOperationLogs()
        {
            await _logService.ClearOperationLogs();
        }
    }
}
