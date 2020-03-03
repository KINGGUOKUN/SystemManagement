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
using WeihanLi.EntityFramework;
using WeihanLi.Extensions;

namespace SystemManagement.Service
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly UserContext _currentUser;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRelationRepository _relationRepository;

        public RoleService(IMapper mapper,
            UserContext currentUser, 
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

        public async Task<PagedModel<SysRoleDto>> SearchRoles(RoleSearchModel searchModel)
        {
            Expression<Func<SysRole, bool>> whereCondition = x => true;
            if (!string.IsNullOrWhiteSpace(searchModel.RoleName))
            {
                whereCondition = whereCondition.And(x => x.Name.Contains(searchModel.RoleName));
            }

            var pagedModel = await _roleRepository.PagedAsync(searchModel.PageIndex, searchModel.PageSize, whereCondition, x => x.ID, true);

            return _mapper.Map<PagedModel<SysRoleDto>>(pagedModel);
        }

        public async Task<dynamic> GetRoleTreeListByUserId(long userId)
        {
            dynamic result = null;
            IEnumerable<ZTreeNode<long, dynamic>> treeNodes = null;
            var user = await _userRepository.FetchAsync(x => x.ID == userId);
            var roles = await _roleRepository.SelectAsync(x => true);
            var roleIds = user.RoleId.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => long.Parse(x)) ?? new List<long>();
            if (roles.Any())
            {
                treeNodes = roles.Select(x => new ZTreeNode<long, dynamic>
                {
                    ID = x.ID,
                    PID = x.PID.HasValue ? x.PID.Value : 0,
                    Name = x.Name,
                    Open = x.PID.HasValue && x.PID.Value > 0 ? false : true,
                    Checked = roleIds.Contains(x.ID)
                });

                result = new
                {
                    treeData = treeNodes.Select(x => new Node<long>
                    {
                        ID = x.ID,
                        PID = x.PID,
                        Name = x.Name,
                        Checked = x.Checked
                    }),
                    checkedIds = treeNodes.Where(x => x.Checked).Select(x => x.ID)
                };
            }

            return result;
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
