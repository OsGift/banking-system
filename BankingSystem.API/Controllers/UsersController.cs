using BankingSystem.API.Middleware;
using BankingSystem.Application.DTOs;
using BankingSystem.Application.Interfaces;
using BankingSystem.Domain.Entities;
using BankingSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace BankingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthenticationService _authenticationService;

        public UserController(IUserRepository userRepository, AuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Add a new user.
        /// </summary>
        /// <param name="user">The user details to create.</param>
        /// <returns>The created user.</returns>
        [HttpPost]
        [SwaggerResponse(201, "User created successfully", typeof(ResponseType<User>))]
        [SwaggerResponse(400, "Bad request", typeof(ResponseType<User>))]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest(ResponseType<User>.Failure("User data is required"));

            var createdUser = await _userRepository.AddAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId }, ResponseType<User>.Success(createdUser, "User created successfully"));
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>A list of users.</returns>
        [HttpGet]
        [SwaggerResponse(200, "Users fetched successfully", typeof(ResponseType<IEnumerable<User>>))]
        [RoleRequirement("Admin", "Manager")] // Admins and Managers can create accounts
        [Authorize]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(ResponseType<IEnumerable<User>>.Success(users, "Users fetched successfully"));
        }

        /// <summary>
        /// Get user by ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The user details.</returns>
        [HttpGet("{id}")]
        [SwaggerResponse(200, "User fetched successfully", typeof(ResponseType<User>))]
        [SwaggerResponse(404, "User not found", typeof(ResponseType<User>))]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound(ResponseType<User>.Failure($"User with ID {id} not found"));

            return Ok(ResponseType<User>.Success(user, "User fetched successfully"));
        }

        /// <summary>
        /// Login to the system (authenticate user).
        /// </summary>
        /// <param name="loginRequest">Login request with username and password.</param>
        /// <returns>A JWT token if authentication is successful.</returns>
        [HttpPost("login")]
        [SwaggerResponse(200, "Login successful", typeof(ResponseType<object>))]
        [SwaggerResponse(400, "Bad request", typeof(ResponseType<object>))]
        [SwaggerResponse(401, "Invalid credentials", typeof(ResponseType<object>))]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
                return BadRequest(ResponseType<object>.Failure("Username and Password are required"));

            var user = await _userRepository.LoginAsync(loginRequest.Username, loginRequest.Password);

            if (user == null)
                return Unauthorized(ResponseType<object>.Failure("Invalid credentials"));

            // Generate the JWT token
            var token = _authenticationService.GenerateToken(user.Username, user.Role);

            return Ok(ResponseType<object>.Success(new { Token = token }, "Login successful"));
        }

        /// <summary>
        /// Change the role of a user (Admin only).
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <param name="newRole">The new role to assign.</param>
        /// <returns>A success or failure message.</returns>
        [HttpPut("{id}/role")]
        [RoleRequirement("Admin")]
        [SwaggerResponse(200, "Role updated successfully", typeof(ResponseType<object>))]
        [SwaggerResponse(400, "Bad request", typeof(ResponseType<object>))]
        [SwaggerResponse(404, "User not found", typeof(ResponseType<object>))]
        public async Task<IActionResult> ChangeRole(Guid id, [FromBody] string newRole)
        {
            if (string.IsNullOrEmpty(newRole))
                return BadRequest(ResponseType<object>.Failure("Role is required"));

            var result = await _userRepository.ChangeRoleAsync(id, newRole);
            if (!result)
                return NotFound(ResponseType<object>.Failure($"User with ID {id} not found"));

            return Ok(ResponseType<object>.Success("Role updated successfully"));
        }

        /// <summary>
        /// Delete a user by ID (Admin only).
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>No content if the user is deleted.</returns>
        [HttpDelete("{id}")]
        [RoleRequirement("Admin")]
        [SwaggerResponse(204, "User deleted successfully", typeof(ResponseType<object>))]
        [SwaggerResponse(404, "User not found", typeof(ResponseType<object>))]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _userRepository.DeleteAsync(id);
            if (!result)
                return NotFound(ResponseType<object>.Failure($"User with ID {id} not found"));

            return NoContent();
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
