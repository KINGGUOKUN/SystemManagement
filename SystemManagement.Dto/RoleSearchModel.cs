using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
    /// <summary>
    /// 角色检索条件
    /// </summary>
    public class RoleSearchModel : BaseSearchModel
    {
        /// <summary>
        /// 角色名
        /// </summary>
        public string RoleName { get; set; }
    }
}
