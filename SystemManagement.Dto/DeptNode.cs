using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
    /// <summary>
    /// 部门节点
    /// </summary>
    public class DeptNode : SysDeptDto
    {
        /// <summary>
        /// 子部门
        /// </summary>
        public List<DeptNode> Children { get; private set; } = new List<DeptNode>();
    }
}
