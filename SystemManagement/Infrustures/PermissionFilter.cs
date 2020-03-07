using Microsoft.AspNetCore.Authorization;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc.Filters
{
    public class PermissionFilter : Attribute, IAsyncAuthorizationFilter
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly PermissionRequirement _requirement;

        public PermissionFilter(IAuthorizationService authorizationService, PermissionRequirement requirement)
        {
            _authorizationService = authorizationService;
            _requirement = requirement;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var result = await _authorizationService.AuthorizeAsync(context.HttpContext.User, null, _requirement);

            if (!result.Succeeded)
            {
                context.Result = new JsonResult("您没有此操作权限")
                {
                    StatusCode = (int)HttpStatusCode.Forbidden
                };
            }
        }
    }
}
