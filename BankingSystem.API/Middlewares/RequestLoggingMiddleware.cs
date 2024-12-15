using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BankingSystem.API.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation($"Incoming Request: {context.Request.Method} {context.Request.Path}");
            await _next(context);
            _logger.LogInformation($"Outgoing Response: {context.Response.StatusCode}");
        }
    }
}
