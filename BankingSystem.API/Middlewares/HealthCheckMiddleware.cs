using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BankingSystem.API.Middleware
{
    public class HealthCheckMiddleware
    {
        private readonly RequestDelegate _next;

        public HealthCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/health")
            {
                context.Response.StatusCode = 200;
                await context.Response.WriteAsync("Healthy");
                return;
            }

            await _next(context);
        }
    }
}
