using BankingSystem.Application.Commands.Transactions;
using BankingSystem.Application.Queries.Accounts.BankingSystem.Application.Queries.Transactions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] DepositCommand command)
        {
            var result = await _mediator.Send(command);
            return result ? Ok("Deposit successful.") : BadRequest("Failed to deposit.");
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawCommand command)
        {
            var result = await _mediator.Send(command);
            return result ? Ok("Withdrawal successful.") : BadRequest("Failed to withdraw.");
        }

        [HttpGet("{accountId}/history")]
        public async Task<IActionResult> GetTransactionHistory(Guid accountId)
        {
            var query = new GetTransactionHistoryQuery { AccountId = accountId };
            var transactions = await _mediator.Send(query);
            return Ok(transactions);
        }
    }
}
