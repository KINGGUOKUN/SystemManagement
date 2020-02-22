using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SystemManagement.Common;
using SystemManagement.Dto;
using SystemManagement.Entity;
using SystemManagement.Repository.Contract;
using SystemManagement.Service.Contract;
using WeihanLi.Common.Helpers;
using WeihanLi.EntityFramework;

namespace SystemManagement.Service
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly SysUserDto _currentUser;
        private readonly IUserRepository _userRepository;

        public AccountService(IMapper mapper, SysUserDto currentUser, IUserRepository userRepository)
        {
            _mapper = mapper;
            _currentUser = currentUser;
            _userRepository = userRepository;
        }

        public async Task<SysUserDto> GetCurrentUserInfo()
        {
            var user = await _userRepository.FetchAsync(x => x.ID == _currentUser.ID);

            return _mapper.Map<SysUserDto>(user);
        }

        public async Task UpdatePassword(ChangePasswordDto passwordDto)
        {
            if (string.Equals(_currentUser.Account, "admin", StringComparison.OrdinalIgnoreCase))
            {
                throw new BusinessException((int)ErrorCode.Forbidden, "不能修改超级管理员密码");
            }

            if (string.IsNullOrWhiteSpace(passwordDto.Password) || string.IsNullOrWhiteSpace(passwordDto.RePassword))
            {
                throw new BusinessException((int)ErrorCode.BadRequest, "密码不能为空");
            }

            if (!string.Equals(passwordDto.Password, passwordDto.RePassword))
            {
                throw new BusinessException((int)ErrorCode.Forbidden, "新密码前后不一致");
            }

            var user = await _userRepository.FetchAsync(x => x.ID == _currentUser.ID);
            if (!string.Equals(HashHelper.GetHashedString(HashType.MD5, passwordDto.OldPassword + user.Salt), user.Password, StringComparison.OrdinalIgnoreCase))
            {
                throw new BusinessException((int)ErrorCode.Forbidden, "旧密码输入错误");
            }

            user.Password = passwordDto.Password;
            await _userRepository.UpdateAsync(user, x => x.Password);            
        }

        public async Task<Tuple<bool, SysUserDto>> ValidateCredentials(string account, string password)
        {
            var user = await _userRepository.FetchAsync(x => x.Account == account);
            if (user != null)
            {
                if (HashHelper.GetHashedString(HashType.MD5, user.Password) == password)
                {
                    return Tuple.Create(true, _mapper.Map<SysUserDto>(user));
                }
            }

            return Tuple.Create<bool, SysUserDto>(false, null);
        }
    }
}
