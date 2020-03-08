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

        [HttpGet("list")]
        public async Task<PagedModel<SysRoleDto>> SearchRoles([FromQuery]RoleSearchModel searchModel)
        {
            return await _roleService.SearchRoles(searchModel);
        }

        [HttpGet("roleTreeListByUserId/{userId}")]
        public async Task<dynamic> GetRoleTreeListByUserId(long userId)
        {
            return await _roleService.GetRoleTreeListByUserId(userId);
        }

        [HttpDelete("{roleId}")]
        [Permission("roleDelete")]
        public async Task RemoveRole(long roleId)
        {
            await _roleService.RemoveRole(roleId);
        }

        [HttpPost("savePermisson")]
        [Permission("roleSetAuthority")]
        public async Task SavePermisson(SysRoleDto roleDto)
        {
            await _roleService.SavePermisson(roleDto.ID, roleDto.Permissions);
        }

        [HttpPost]
        [Permission("roleAdd", "roleEdit")]
        public async Task SaveRole(SysRoleDto roleDto)
        {
            await _roleService.SaveRole(roleDto);
        }
    }
}