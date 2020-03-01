using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SystemManagement.Dto;

namespace SystemManagement.Service.Contract
{
    public interface IMenuService : IService
    {
        Task<List<MenuNode>> GetMenus();

        Task<List<RouterMenu>> GetMenusForRouter();

        Task<dynamic> GetMenuTreeListByRoleId(long roleId);

        Task SaveMenu(SysMenuDto menuDto);

        Task DeleteMenu(long menuId);
    }
}
