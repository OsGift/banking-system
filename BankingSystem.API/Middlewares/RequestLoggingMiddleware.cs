using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Diagnostics;
using System.IO;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text.Json;
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
            var stopWatch = Stopwatch.StartNew();

            // Log Incoming Request Details
            _logger.LogInformation($"Incoming Request: {context.Request.Method} {context.Request.Path}");
            _logger.LogInformation($"Request Headers: {JsonSerializer.Serialize(context.Request.Headers)}");

            // Log who made the request if there is a token in the Authorization header
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
                    var userName = jwtToken?.Claims.FirstOrDefault(c => c.Type == "username")?.Value;
                    var userId = jwtToken?.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;

                    if (userName != null)
                    {
                        _logger.LogInformation($"Request made by User: {userName} (UserID: {userId})");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Failed to parse token: {ex.Message}");
                }
            }

            // Read and Log Request Body if applicable
            if (context.Request.ContentLength > 0)
            {
                context.Request.EnableBuffering();
                using var reader = new StreamReader(context.Request.Body);
                var body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0; // Reset the body stream position so the next middleware can read it
                _logger.LogInformation($"Request Body: {body}");
            }

            // Continue with the request processing
            await _next(context);

            stopWatch.Stop();

            // Log Outgoing Response Details
            _logger.LogInformation($"Outgoing Response: {context.Response.StatusCode}");
            _logger.LogInformation($"Response Time: {stopWatch.ElapsedMilliseconds} ms");
            _logger.LogInformation($"Response Headers: {JsonSerializer.Serialize(context.Response.Headers)}");
        }
    }
}
