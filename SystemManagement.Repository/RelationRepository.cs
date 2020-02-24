using System;
using System.Collections.Generic;
using System.Text;
using SystemManagement.Entity;
using SystemManagement.Repository.Contract;
using WeihanLi.EntityFramework;

namespace SystemManagement.Repository
{
    public class RelationRepository : EFRepository<SystemManageDbContext, SysRelation>, IRelationRepository
    {
        public RelationRepository(SystemManageDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
