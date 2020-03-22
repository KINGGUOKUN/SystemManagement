using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemManagement.Dto;
using SystemManagement.Service.Contract;

namespace SystemManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Permission("menu")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<List<MenuNode>> GetMenus()
        {
            return await _menuService.GetMenus();
        }

        /// <summary>
        /// 获取侧边栏路由菜单
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("listForRouter")]
        public async Task<List<RouterMenu>> GetMenusForRouter()
        {
            return await _menuService.GetMenusForRouter();
        }

        /// <summary>
        /// 根据角色获取菜单树
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        [HttpGet("menuTreeListByRoleId/{roleId}")]
        public async Task<dynamic> GetMenuTreeListByRoleId(long roleId)
        {
            return await _menuService.GetMenuTreeListByRoleId(roleId);
        }

        /// <summary>
        /// 保存菜单信息
        /// </summary>
        /// <param name="menuDto">菜单</param>
        /// <returns></returns>
        [HttpPost]
        [Permission("menuAdd", "menuEdit")]
        public async Task SaveMenu(SysMenuDto menuDto)
        {
            await _menuService.SaveMenu(menuDto);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <returns></returns>
        [HttpDelete("{menuId}")]
        [Permission("menuDelete")]
        public async Task DeleteMenu(long menuId)
        {
            await _menuService.DeleteMenu(menuId);
        }
    }
}