using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SystemManagement.Entity;
using WeihanLi.EntityFramework;

namespace SystemManagement.Repository.Contract
{
    public interface IOperationLogRepository : IEFRepository<SystemManageDbContext, SysOperationLog>
    {
        Task ClearLogs();
    }
}
