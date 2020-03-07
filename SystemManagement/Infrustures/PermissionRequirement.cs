namespace Microsoft.AspNetCore.Authorization
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(params string[] codes)
        {
            this.Codes = codes;
        }

        public string[] Codes { get; private set; }
    }
}
