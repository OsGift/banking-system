using BankingSystem.API.Middleware;
using BankingSystem.Application.Commands.Accounts;
using BankingSystem.Application.DTOs;
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
        public async Task<ActionResult<ResponseType<AccountDto>>> CreateAccount([FromBody] CreateAccountCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result); // Return the full response, including the DTO and message
            }
            else
            {
                return BadRequest(result); // Return the failure response with the error message
            }
        }


        [HttpGet("{accountNumber}")]
        [RoleRequirement("Admin", "User")] // Admins and Users can view account details
        public async Task<ActionResult<ResponseType<AccountDto>>> GetAccountDetails(string accountNumber)
        {
            var query = new GetAccountDetailsQuery { AccountNumber = accountNumber };
            var account = await _mediator.Send(query);
            return account != null ? Ok(account) : NotFound("Account not found.");
        }
    }
}
