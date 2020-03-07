using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            var roles = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            if (!string.IsNullOrWhiteSpace(roles))
            {
                var roleIds = roles.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => long.Parse(x));
                if (roleIds.Contains(1))
                {
                    context.Succeed(requirement);
                    return;
                }

                var menus = await _menuService.GetMenusByRoleIds(roleIds.ToArray());
                if (menus.Any())
                {
                    var codes = menus.Select(x => x.Code.ToUpper());
                    if (requirement.Codes.Any(x => codes.Contains(x.ToUpper())))
                    {
                        context.Succeed(requirement);
                    }
                }
            }
        }
    }
}
