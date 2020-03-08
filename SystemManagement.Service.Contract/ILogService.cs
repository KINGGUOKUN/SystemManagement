using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SystemManagement.Common;
using SystemManagement.Dto;

namespace SystemManagement.Service.Contract
{
    public interface ILogService : IService
    {
        Task<PagedModel<SysOperationLogDto>> SearchOperationLogs(LogSearchModel searchModel);

        Task AppendOperationLog(SysOperationLogDto logDto);

        Task ClearOperationLogs();
    }
}
