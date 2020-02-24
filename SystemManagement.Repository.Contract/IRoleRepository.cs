using System;
using System.Collections.Generic;
using System.Text;
using SystemManagement.Entity;
using WeihanLi.EntityFramework;

namespace SystemManagement.Repository.Contract
{
    public interface IRoleRepository : IEFRepository<SystemManageDbContext, SysRole>
    {
    }
}
