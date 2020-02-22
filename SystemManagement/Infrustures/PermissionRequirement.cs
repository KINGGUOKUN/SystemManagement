namespace Microsoft.AspNetCore.Authorization
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(string module, string button)
        {
            this.Module = module;
            this.Button = button;
        }

        public string Module { get; private set; }

        public string Button { get; private set; }
    }
}
