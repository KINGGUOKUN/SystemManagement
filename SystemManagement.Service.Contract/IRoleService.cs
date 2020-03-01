using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SystemManagement.Common;
using SystemManagement.Dto;

namespace SystemManagement.Service.Contract
{
    public interface IRoleService : IService
    {
        Task<List<SysRoleDto>> SearchRoles(string roleName);

        Task SaveRole(SysRoleDto roleDto);

        Task RemoveRole(long roleId);

        Task<dynamic> GetRoleTreeListByUserId(long UserId);

        Task SavePermisson(long roleId, string permissions);
    }
}
