using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class DeptService : IDeptService
    {
        private readonly IMapper _mapper;
        private readonly IDeptRepository _deptRepository;

        public DeptService(IMapper mapper, IDeptRepository deptRepository)
        {
            _mapper = mapper;
            _deptRepository = deptRepository;
        }

        public async Task DeleteDept(long deptId)
        {
            var dept = await _deptRepository.FetchAsync(x => x.ID == deptId);
            await _deptRepository.DeleteAsync(x => x.Pids.Contains($"[{dept.ID}]"));
            await _deptRepository.DeleteAsync(dept);
        }

        public async Task<List<DeptNode>> GetDeptList()
        {
            List<DeptNode> result = new List<DeptNode>();

            var depts = await _deptRepository.SelectAsync(x => true);
            if (depts.Any())
            {
                var deptNodes = _mapper.Map<List<DeptNode>>(depts);
                var dictDepts = deptNodes.ToDictionary(x => x.ID);
                foreach (var pair in dictDepts)
                {
                    var currentDept = pair.Value;
                    var parentDept = deptNodes.FirstOrDefault(x => x.ID == currentDept.Pid);
                    if (parentDept != null)
                    {
                        parentDept.Children.Add(currentDept);
                    }
                    else
                    {
                        result.Add(currentDept);
                    }
                }
            }

            return result;
        }

        public async Task SaveDept(SysDeptDto deptDto)
        {
            if (string.IsNullOrWhiteSpace(deptDto.FullName))
            {
                throw new BusinessException((int)ErrorCode.BadRequest, "请输入部门全称");
            }

            if (string.IsNullOrWhiteSpace(deptDto.SimpleName))
            {
                throw new BusinessException((int)ErrorCode.BadRequest, "请输入部门简称");
            }

            if (deptDto.ID == 0)
            {
                var dept = _mapper.Map<SysDept>(deptDto);
                await this.SetDeptPids(dept);
                await _deptRepository.InsertAsync(dept);
            }
            else
            {
                var oldDept = await _deptRepository.FetchAsync(x => x.ID == deptDto.ID);
                oldDept.Pid = deptDto.Pid;
                oldDept.SimpleName = deptDto.SimpleName;
                oldDept.FullName = deptDto.FullName;
                oldDept.Num = deptDto.Num;
                oldDept.Tips = deptDto.Tips;
                await this.SetDeptPids(oldDept);

                await _deptRepository.UpdateAsync(oldDept);
            }
        }

        private async Task SetDeptPids(SysDept sysDept)
        {
            if (sysDept.Pid.HasValue && sysDept.Pid.Value > 0)
            {
                var dept = await _deptRepository.FetchAsync(x => x.ID == sysDept.Pid.Value);
                string pids = dept?.Pids ?? "";
                sysDept.Pids = $"{pids}[{sysDept.Pid}],";
            }
            else
            {
                sysDept.Pid = 0;
                sysDept.Pids = "[0],";
            }
        }
    }
}
