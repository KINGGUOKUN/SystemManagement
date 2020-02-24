using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SystemManagement.Common;
using SystemManagement.Dto;
using SystemManagement.Entity;
using SystemManagement.Repository.Contract;
using SystemManagement.Service.Contract;
using WeihanLi.EntityFramework;

namespace SystemManagement.Service
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly SysUserDto _currentUser;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRelationRepository _relationRepository;

        public RoleService(IMapper mapper, 
            SysUserDto currentUser, 
            IRoleRepository roleRepository, 
            IUserRepository userRepository,
            IRelationRepository relationRepository)
        {
            _mapper = mapper;
            _currentUser = currentUser;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _relationRepository = relationRepository;
        }

        public async Task<List<SysRoleDto>> SearchRoles(string roleName)
        {
            List<SysRole> roles = null;
            if(string.IsNullOrWhiteSpace(roleName))
            {
                roles = await _roleRepository.GetAsync(q => q.WithPredict(x => true));
            }
            else
            {
                roles = await _roleRepository.GetAsync(q => q.WithPredict(x => x.Name.Contains(roleName)));
            }

            return _mapper.Map<List<SysRoleDto>>(roles);
        }

        public async Task GetRoleTreeListByIdUser(long roleId)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveRole(long roleId)
        {
            if (roleId < 2)
            {
                throw new BusinessException((int)ErrorCode.Forbidden, "不能删除初始角色");
            }

            if (await _userRepository.ExistAsync(x => x.RoleId == roleId.ToString()))
            {
                throw new BusinessException((int)ErrorCode.Forbidden, "有用户使用该角色，禁止删除");
            }

            await _roleRepository.DeleteAsync(x => x.ID == roleId);
        }

        public async Task SavePermisson(long roleId, string permissions)
        {
            await _relationRepository.DeleteAsync(x => x.ID == roleId);

            string[] permissionIds = permissions.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var permissionId in permissions)
            {
                SysRelation relation = new SysRelation
                {
                    RoleId = roleId,
                    MenuId = permissionId
                };
                await _relationRepository.InsertAsync(relation);
            }
        }

        public async Task SaveRole(SysRoleDto roleDto)
        {
            var role = _mapper.Map<SysRole>(roleDto);
            if (roleDto.ID < 1)
            {
                await _roleRepository.InsertAsync(role);
            }
            else
            {
                await _roleRepository.UpdateAsync(role);
            }
        }
    }
}
