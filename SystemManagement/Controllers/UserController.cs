using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SystemManagement.Common;
using SystemManagement.Dto;
using SystemManagement.Service.Contract;

namespace SystemManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Permission("mgr")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="searchModel">用户查询条件</param>
        /// <returns></returns>
        [Log("查询用户")]
        [HttpGet("list")]
        public async Task<PagedModel<SysUserDto>> SearchUsers([FromQuery]UserSearchModel searchModel)
        {
            return await _userService.SearchUsers(searchModel);
        }

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="userDto">用户信息</param>
        /// <returns></returns>
        [HttpPost]
        [Permission("mgrAdd", "mgrEdit")]
        public async Task SaveUser(SysUserDto userDto)
        {
            await _userService.SaveUser(userDto);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        [HttpDelete("{userId}")]
        [Permission("mgrDelete")]
        public async Task RemoveUser(long userId)
        {
            await _userService.RemoveUser(userId);
        }

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="userDto">用户-角色信息</param>
        /// <returns></returns>
        [HttpPut("setRole")]
        [Permission("mgrSetRole")]
        public async Task SetRole(SysUserDto userDto)
        {
            await _userService.SetRole(userDto.ID, userDto.RoleId);
        }

        /// <summary>
        /// 变更用户状态
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        [HttpPut("changeStatus/{userId}")]
        [Permission("mgrFreeze")]
        public async Task ChangeStatus(long userId)
        {
            await _userService.ChangeStatus(userId);
        }
    }
}