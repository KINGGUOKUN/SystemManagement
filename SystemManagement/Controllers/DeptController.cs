using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemManagement.Dto;
using SystemManagement.Service.Contract;

namespace SystemManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeptController : ControllerBase
    {
        private readonly IDeptService _deptService;

        public DeptController(IDeptService deptService)
        {
            _deptService = deptService;
        }

        [HttpDelete("{deptId}")]
        public async Task DeleteDept(long deptId)
        {
            await _deptService.DeleteDept(deptId);
        }

        [HttpGet("list")]
        public async Task<List<DeptNode>> GetDeptList()
        {            
            return await _deptService.GetDeptList();
        }

        [HttpPost]
        public async Task SaveDept(SysDeptDto deptDto)
        {
            await _deptService.SaveDept(deptDto);
        }
    }
}