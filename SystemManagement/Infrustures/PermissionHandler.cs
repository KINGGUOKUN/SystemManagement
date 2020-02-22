using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemManagement.Service.Contract;

namespace Microsoft.AspNetCore.Authorization
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IMenuService _menuService;

        public PermissionHandler(IMenuService menuService)
        {
            _menuService = menuService;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            //var roles = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            //if (roles.Contains(Guid.Empty.ToString()))
            //{
            //    context.Succeed(requirement);
            //    return Task.CompletedTask;
            //}

            //var menus = _menuService.GetMenusByUserId(context.User.Identity.Name);
            //if (menus.Any())
            //{
            //    var module = menus.FirstOrDefault(x => string.Equals(x.Name, requirement.Module));
            //    if (module != null)
            //    {
            //        var button = menus.FirstOrDefault(x => x.ParentId == module.ID && string.Equals(x.Name, requirement.Button));
            //        if (button != null)
            //        {
            //            context.Succeed(requirement);
            //        }
            //    }
            //}

            return Task.CompletedTask;
        }
    }
}
