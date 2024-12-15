using BankingSystem.API.Middleware;
using BankingSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;

        public UsersController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            // Validate user credentials (dummy logic for example)
            if (username == "admin" && password == "password")
            {
                var token = _authenticationService.GenerateToken(username, "Admin");
                return Ok(new { Token = token });
            }

            if (username == "user" && password == "password")
            {
                var token = _authenticationService.GenerateToken(username, "User");
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid credentials");
        }

        [HttpGet("admin-data")]
        [RoleRequirement("Admin")] // Only Admins can access
        public IActionResult GetAdminData()
        {
            return Ok("This is sensitive admin data.");
        }
    }
}
