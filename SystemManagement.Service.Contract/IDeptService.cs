using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SystemManagement.Dto;

namespace SystemManagement.Service.Contract
{
    public interface IDeptService : IService
    {
        Task<List<DeptNode>> GetDeptList();

        Task SaveDept(SysDeptDto deptDto);

        Task DeleteDept(long deptId);
    }
}
