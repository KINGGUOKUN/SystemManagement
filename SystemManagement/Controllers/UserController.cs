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
        public async Task SaveUser(SysUserDto userDto)
        {
            await _userService.SaveUser(userDto);
        }

        [HttpDelete]
        public async Task RemoveUser(long ID)
        {
            await _userService.RemoveUser(ID);
        }

        [HttpPut("setRole")]
        public async Task SetRole(long userId, string roleIds)
        {
            await _userService.SetRole(userId, roleIds);
        }

        [HttpPut("changeStatus/{userId}")]
        public async Task ChangeStatus(long userId)
        {
            await _userService.ChangeStatus(userId);
        }
    }
}