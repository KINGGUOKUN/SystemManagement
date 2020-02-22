using System;
using System.Collections.Generic;
using System.Text;
using SystemManagement.Entity;
using SystemManagement.Repository.Contract;
using WeihanLi.EntityFramework;

namespace SystemManagement.Repository
{
    public class UserRepository : EFRepository<SystemManageDbContext, SysUser>, IUserRepository
    {
        public UserRepository(SystemManageDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
