using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemManagement.Dto;
using SystemManagement.Service.Contract;

namespace SystemManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Permission("menu")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet("list")]
        public async Task<List<MenuNode>> GetMenus()
        {
            return await _menuService.GetMenus();
        }

        [HttpGet("listForRouter")]
        public async Task<List<RouterMenu>> GetMenusForRouter()
        {
            return await _menuService.GetMenusForRouter();
        }

        [HttpGet("menuTreeListByRoleId/{roleId}")]
        public async Task<dynamic> GetMenuTreeListByRoleId(long roleId)
        {
            return await _menuService.GetMenuTreeListByRoleId(roleId);
        }

        [HttpPost]
        [Permission("menuAdd", "menuEdit")]
        public async Task SaveMenu(SysMenuDto menuDto)
        {
            await _menuService.SaveMenu(menuDto);
        }

        [HttpDelete("{menuId}")]
        [Permission("menuDelete")]
        public async Task DeleteMenu(long menuId)
        {
            await _menuService.DeleteMenu(menuId);
        }
    }
}