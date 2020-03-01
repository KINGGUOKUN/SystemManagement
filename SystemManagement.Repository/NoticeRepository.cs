using System;
using System.Collections.Generic;
using System.Text;
using SystemManagement.Entity;
using SystemManagement.Repository.Contract;
using WeihanLi.EntityFramework;

namespace SystemManagement.Repository
{
    public class NoticeRepository : EFRepository<SystemManageDbContext, SysNotice>, INoticeRepository
    {
        public NoticeRepository(SystemManageDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
