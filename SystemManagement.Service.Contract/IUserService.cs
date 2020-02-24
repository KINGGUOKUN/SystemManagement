using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SystemManagement.Common;
using SystemManagement.Dto;

namespace SystemManagement.Service.Contract
{
    public interface IUserService : IService
    {
        Task<PagedModel<SysUserDto>> SearchUsers(UserSearchModel searchModel);

        Task SaveUser(SysUserDto userDto);

        Task RemoveUser(long userId);

        Task SetRole(long userId, string roleIds);

        Task ChangeStatus(long userId);
    }
}
