using System;
using System.Collections.Generic;
using System.Text;
using SystemManagement.Entity;
using SystemManagement.Repository.Contract;
using WeihanLi.EntityFramework;

namespace SystemManagement.Repository
{
    public class RoleRepository : EFRepository<SystemManageDbContext, SysRole>, IRoleRepository
    {
        public RoleRepository(SystemManageDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
