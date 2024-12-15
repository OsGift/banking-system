using BankingSystem.API.Middleware;
using BankingSystem.Application.Commands.Accounts;
using BankingSystem.Application.Queries.Accounts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        [RoleRequirement("Admin", "Manager")] // Admins and Managers can create accounts
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountCommand command)
        {
            var result = await _mediator.Send(command);
            return result ? Ok("Account created successfully.") : BadRequest("Failed to create account.");
        }

        [HttpGet("{accountId}")]
        [RoleRequirement("Admin", "User")] // Admins and Users can view account details
        public async Task<IActionResult> GetAccountDetails(Guid accountId)
        {
            var query = new GetAccountDetailsQuery { AccountId = accountId };
            var account = await _mediator.Send(query);
            return account != null ? Ok(account) : NotFound("Account not found.");
        }
    }
}
