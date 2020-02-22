using System;
using System.Collections.Generic;
using System.Text;
using SystemManagement.Entity;
using SystemManagement.Repository.Contract;
using WeihanLi.EntityFramework;

namespace SystemManagement.Repository
{
    public class MenuRepository : EFRepository<SystemManageDbContext, SysMenu>, IMenuRepository
    {
        public MenuRepository(SystemManageDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
