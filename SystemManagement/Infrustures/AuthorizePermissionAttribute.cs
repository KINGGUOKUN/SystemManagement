using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    /// <summary>
    /// 权限特性
    /// </summary>
    public class PermissionAttribute : TypeFilterAttribute
    {
        public PermissionAttribute(params string[] codes)
            : base(typeof(PermissionFilter))
        {
            Arguments = new[] { new PermissionRequirement(codes) };
        }
    }
}
