using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Authorization
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            string module = requirement.Module,
                action = requirement.Action;
            if (!string.IsNullOrWhiteSpace(module) && !string.IsNullOrWhiteSpace(action))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
