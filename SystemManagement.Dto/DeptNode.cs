using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
    public class DeptNode : SysDeptDto
    {
        public List<DeptNode> Children { get; private set; } = new List<DeptNode>();
    }
}
