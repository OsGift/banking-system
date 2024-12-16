using BankingSystem.Application.Commands.Transactions;
using BankingSystem.Application.DTOs.BankingSystem.Application.DTOs;
using BankingSystem.Application.DTOs;
using BankingSystem.Application.Queries.Transactions;
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
        public async Task<ActionResult<ResponseType<bool>>> Deposit([FromBody] DepositCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("withdraw")]
        public async Task<ActionResult<ResponseType<bool>>> Withdraw([FromBody] WithdrawCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("{accountNumber}/history")]
        public async Task<ActionResult<ResponseType<List<TransactionDto>>>> GetTransactionHistory(string accountNumber)
        {
            var query = new GetTransactionHistoryQuery { AccountNumber = accountNumber };
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
