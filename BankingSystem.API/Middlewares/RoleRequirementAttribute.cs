using Microsoft.AspNetCore.Authorization;

namespace BankingSystem.API.Middleware
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class RoleRequirementAttribute : Attribute
    {
        public string[] Roles { get; }

        public RoleRequirementAttribute(params string[] roles)
        {
            Roles = roles;
        }
    }
}
