using System;
using System.Collections.Generic;
using System.Text;
using SystemManagement.Entity;
using SystemManagement.Repository.Contract;
using WeihanLi.EntityFramework;

namespace SystemManagement.Repository
{
    public class DeptRepository : EFRepository<SystemManageDbContext, SysDept>, IDeptRepository
    {
        public DeptRepository(SystemManageDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
