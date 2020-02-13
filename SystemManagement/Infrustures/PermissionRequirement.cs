namespace Microsoft.AspNetCore.Authorization
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Module { get; private set; }

        public string Action { get; private set; }

        public PermissionRequirement(string module, string action)
        {
            this.Module = module;
            this.Action = action;
        }
    }
}
