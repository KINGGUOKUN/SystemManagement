using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SystemManagement.Dto;

namespace SystemManagement.Service.Contract
{
    public interface IAccountService : IService
    {
        Task<Tuple<bool, SysUserDto>> ValidateCredentials(string account, string password);

        Task<UserInfo> GetCurrentUserInfo();

        Task UpdatePassword(ChangePasswordDto passwordDto);
    }
}
