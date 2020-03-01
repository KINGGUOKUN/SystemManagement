using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<List<SysMenu>> GetMenusByRoleIds(long[] roleIds, bool enabledOnly)
        {
            IQueryable<SysMenu> menus = null;
            if (enabledOnly)
            {
                menus = from r in DbContext.Set<SysRelation>()
                        join m in DbContext.Set<SysMenu>() on r.MenuId equals m.ID
                        where roleIds.Contains(r.ID) && m.Status
                        select m;

            }
            else
            {
                menus = from r in DbContext.Set<SysRelation>()
                        join m in DbContext.Set<SysMenu>() on r.MenuId equals m.ID
                        where roleIds.Contains(r.ID)
                        select m;
            }

            return await menus.ToListAsync();
        }
    }
}
