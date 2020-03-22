using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemManagement.Common;
using SystemManagement.Dto;
using SystemManagement.Service.Contract;

namespace SystemManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Permission("role")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// 查询角色
        /// </summary>
        /// <param name="searchModel">角色查询条件</param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<PagedModel<SysRoleDto>> SearchRoles([FromQuery]RoleSearchModel searchModel)
        {
            return await _roleService.SearchRoles(searchModel);
        }

        /// <summary>
        /// 根据用户ID获取角色树
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        [HttpGet("roleTreeListByUserId/{userId}")]
        public async Task<dynamic> GetRoleTreeListByUserId(long userId)
        {
            return await _roleService.GetRoleTreeListByUserId(userId);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        [HttpDelete("{roleId}")]
        [Permission("roleDelete")]
        public async Task RemoveRole(long roleId)
        {
            await _roleService.RemoveRole(roleId);
        }

        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <param name="roleDto">角色-权限信息</param>
        /// <returns></returns>
        [HttpPost("savePermisson")]
        [Permission("roleSetAuthority")]
        public async Task SavePermisson(SysRoleDto roleDto)
        {
            await _roleService.SavePermisson(roleDto.ID, roleDto.Permissions);
        }

        /// <summary>
        /// 保存角色信息
        /// </summary>
        /// <param name="roleDto">角色</param>
        /// <returns></returns>
        [HttpPost]
        [Permission("roleAdd", "roleEdit")]
        public async Task SaveRole(SysRoleDto roleDto)
        {
            await _roleService.SaveRole(roleDto);
        }
    }
}