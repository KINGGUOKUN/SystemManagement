using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly UserContext _currentUser;
        private readonly IUserRepository _userRepository;
        private readonly IDeptRepository _deptRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IRelationRepository _relationRepository;

        public AccountService(IMapper mapper,
            UserContext currentUser, 
            IUserRepository userRepository, 
            IDeptRepository deptRepository, 
            IRoleRepository roleRepository, 
            IMenuRepository menuRepository, 
            IRelationRepository relationRepository)
        {
            _mapper = mapper;
            _currentUser = currentUser;
            _userRepository = userRepository;
            _deptRepository = deptRepository;
            _roleRepository = roleRepository;
            _menuRepository = menuRepository;
            _relationRepository = relationRepository;
        }

        public async Task<UserInfo> GetCurrentUserInfo()
        {
            var user = await _userRepository.FetchAsync(x => x.ID == _currentUser.ID);
            var dept = await _deptRepository.FetchAsync(x => x.ID == user.DeptId);
            UserInfo userContext = new UserInfo
            {
                Name = user.Name,
                Role = "admin"
            };
            userContext.Profile = _mapper.Map<UserProfile>(user);
            userContext.Profile.Dept = dept.FullName;
            if (!string.IsNullOrWhiteSpace(user.RoleId))
            {
                var roleIds = user.RoleId.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x));
                var roles = await _roleRepository.SelectAsync(x => roleIds.Contains(x.ID));
                foreach (var role in roles)
                {
                    userContext.Roles.Add(role.Tips);
                    userContext.Profile.Roles.Add(role.Name);
                }

                var roleMenus = await _menuRepository.GetMenusByRoleIds(roleIds.ToArray(), true);
                if (roleMenus.Any())
                {
                    userContext.Permissions.AddRange(roleMenus.Select(x => x.Url).Distinct());
                }
            }

            return userContext;
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
            if (!string.Equals(HashHelper.GetHashedString(HashType.MD5, passwordDto.OldPassword, user.Salt), user.Password, StringComparison.OrdinalIgnoreCase))
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
                if (HashHelper.GetHashedString(HashType.MD5, password, user.Salt) == user.Password)
                {
                    return Tuple.Create(true, _mapper.Map<SysUserDto>(user));
                }
            }

            return Tuple.Create<bool, SysUserDto>(false, null);
        }
    }
}
