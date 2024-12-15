using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BankingSystem.API.Middleware
{
    public class RBACMiddleware
    {
        private readonly RequestDelegate _next;

        public RBACMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Check if the endpoint has role restrictions
            var endpoint = context.GetEndpoint();
            var requiredRoles = endpoint?.Metadata.GetMetadata<RoleRequirementAttribute>()?.Roles;

            if (requiredRoles != null)
            {
                var userRole = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                if (userRole == null || !requiredRoles.Contains(userRole))
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("Access Denied: Insufficient permissions.");
                    return;
                }
            }

            await _next(context);
        }
    }
}
