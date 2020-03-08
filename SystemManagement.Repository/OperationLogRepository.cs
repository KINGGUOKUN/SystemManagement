using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SystemManagement.Entity;
using SystemManagement.Repository.Contract;
using WeihanLi.EntityFramework;

namespace SystemManagement.Repository
{
    public class OperationLogRepository : EFRepository<SystemManageDbContext, SysOperationLog>, IOperationLogRepository
    {
        public OperationLogRepository(SystemManageDbContext dbContext) 
            : base(dbContext)
        {
        }

        /// <summary>
        /// 清除操作日志
        /// </summary>
        /// <returns></returns>
        public async Task ClearLogs()
        {
            await DbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE SysOperationLog");
        }
    }
}
