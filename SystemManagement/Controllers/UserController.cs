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
    [Permission("mgr")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("list")]
        public async Task<PagedModel<SysUserDto>> SearchUsers([FromQuery]UserSearchModel searchModel)
        {
            return await _userService.SearchUsers(searchModel);
        }

        [HttpPost]
        [Permission("mgrAdd", "mgrEdit")]
        public async Task SaveUser(SysUserDto userDto)
        {
            await _userService.SaveUser(userDto);
        }

        [HttpDelete("{userId}")]
        [Permission("mgrDelete")]
        public async Task RemoveUser(long userId)
        {
            await _userService.RemoveUser(userId);
        }

        [HttpPut("setRole")]
        [Permission("mgrSetRole")]
        public async Task SetRole(SysUserDto userDto)
        {
            await _userService.SetRole(userDto.ID, userDto.RoleId);
        }

        [HttpPut("changeStatus/{userId}")]
        [Permission("mgrFreeze")]
        public async Task ChangeStatus(long userId)
        {
            await _userService.ChangeStatus(userId);
        }
    }
}