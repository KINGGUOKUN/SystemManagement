using AutoMapper;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SystemManagement.Common;
using SystemManagement.Dto;
using SystemManagement.Entity;
using SystemManagement.Repository.Contract;
using SystemManagement.Service.Contract;
using WeihanLi.Common.Helpers;
using WeihanLi.Extensions;
using WeihanLi.EntityFramework;
using System.Collections.Generic;

namespace SystemManagement.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly SysUserDto _currentUser;
        private readonly IUserRepository _userRepository;
        private readonly IDeptRepository _deptRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IMapper mapper, 
            SysUserDto currentUser, 
            IUserRepository userRepository, 
            IDeptRepository deptRepository, 
            IRoleRepository roleRepository)
        {
            _mapper = mapper;
            _currentUser = currentUser;
            _userRepository = userRepository;
            _deptRepository = deptRepository;
            _roleRepository = roleRepository;
        }

        public async Task ChangeStatus(long userId)
        {
            var user = await _userRepository.FetchAsync(x => x.ID == userId);
            user.Status = (int)(user.Status == (int)ManageStatus.Enabled ? ManageStatus.Disabled : ManageStatus.Enabled);
            await _userRepository.UpdateAsync(user, x => x.Status);
        }

        public async Task RemoveUser(long userId)
        {
            if (userId <= 2)
            {
                throw new BusinessException((int)ErrorCode.Forbidden, "不能删除初始用户");
            }

            var user = await _userRepository.FetchAsync(x => x.ID == userId);
            user.Status = (int)ManageStatus.Deleted;

            await _userRepository.UpdateAsync(user, x => x.Status);
        }

        public async Task SaveUser(SysUserDto userDto)
        {
            var user = _mapper.Map<SysUser>(userDto);
            if (user.ID < 1)
            {
                if (await _userRepository.ExistAsync(x => x.Account == user.Account))
                {
                    throw new BusinessException((int)ErrorCode.Forbidden, "用户已存在");
                }

                user.Salt = SecurityHelper.GenerateRandomCode(5);
                user.Password = HashHelper.GetHashedString(HashType.MD5, user.Password, user.Salt);
                user.CreateBy = _currentUser.ID;
                user.CreateTime = DateTime.Now;

                await _userRepository.InsertAsync(user);
            }
            else
            {
                user.ModifyBy = _currentUser.ID;
                user.ModifyTime = DateTime.Now;
                await _userRepository.UpdateAsync(user, 
                    x => x.Name, 
                    x => x.DeptId,
                    x => x.Sex,
                    x => x.Phone,
                    x => x.Email,
                    x => x.Birthday,
                    x => x.Status,
                    x => x.ModifyBy,
                    x => x.ModifyTime);
            }
        }

        public async Task<PagedModel<SysUserDto>> SearchUsers(UserSearchModel searchModel)
        {
            Expression<Func<SysUser, bool>> whereCondition = x => x.Status > 0;
            if (!string.IsNullOrWhiteSpace(searchModel.Account))
            {
                whereCondition = whereCondition.And(x => x.Account.Contains(searchModel.Account));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                whereCondition = whereCondition.And(x => x.Name.Contains(searchModel.Name));
            }

            var pagedModel = await _userRepository.PagedAsync(searchModel.PageIndex, searchModel.PageSize, whereCondition, x => x.ID);
            var result = _mapper.Map<PagedModel<SysUserDto>>(pagedModel);
            if (result.Count > 0)
            {
                var depts = await _deptRepository.SelectAsync(x => true);
                var roles = await _roleRepository.SelectAsync(x => true);
                foreach (var user in result.Data)
                {
                    user.DeptName = depts.FirstOrDefault(x => x.ID == user.DeptId)?.FullName;
                    var roleIds = string.IsNullOrWhiteSpace(user.RoleId) ? new List<long>()
                        : user.RoleId.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x));
                    user.RoleName = string.Join(',', roles.Where(x => roleIds.Contains(x.ID)).Select(x => x.Name));
                }
            }

            return result;
        }

        public async Task SetRole(long userId, string roleIds)
        {
            if(userId == 1)
            {
                throw new BusinessException((int)ErrorCode.Forbidden, "禁止修改管理员角色");
            }

            var user = await _userRepository.FetchAsync(x => x.ID == userId);
            user.RoleId = roleIds;
            await _userRepository.UpdateAsync(user, x => x.RoleId);
        }
    }
}
