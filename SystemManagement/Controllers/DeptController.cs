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

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <returns></returns>
        [HttpDelete("{deptId}")]
        [Permission("deptDelete")]
        public async Task DeleteDept(long deptId)
        {
            await _deptService.DeleteDept(deptId);
        }

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        [Permission("deptList")]
        public async Task<List<DeptNode>> GetDeptList()
        {            
            return await _deptService.GetDeptList();
        }

        /// <summary>
        /// 保存部门信息
        /// </summary>
        /// <param name="deptDto">部门</param>
        /// <returns></returns>
        [HttpPost]
        [Permission("deptAdd", "deptEdit")]
        public async Task SaveDept(SysDeptDto deptDto)
        {
            await _deptService.SaveDept(deptDto);
        }
    }
}