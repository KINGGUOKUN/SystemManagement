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
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("list")]
        public async Task<List<SysRoleDto>> SearchRoles(string roleName)
        {
            return await _roleService.SearchRoles(roleName);
        }

        [HttpGet("roleTreeListByIdUser/{roleId}")]
        public async Task GetRoleTreeListByIdUser(long roleId)
        {
            await _roleService.GetRoleTreeListByIdUser(roleId);
        }

        [HttpDelete("{roleId}")]
        public async Task RemoveRole(long roleId)
        {
            await _roleService.RemoveRole(roleId);
        }

        [HttpPost("savePermisson")]
        public async Task SavePermisson(long roleId, string permissions)
        {
            await _roleService.SavePermisson(roleId, permissions);
        }

        [HttpPost]
        public async Task SaveRole(SysRoleDto roleDto)
        {
            await _roleService.SaveRole(roleDto);
        }
    }
}