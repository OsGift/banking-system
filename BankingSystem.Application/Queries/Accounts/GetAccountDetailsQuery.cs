using BankingSystem.Application.DTOs;
using MediatR;

namespace BankingSystem.Application.Queries.Accounts
{
    public class GetAccountDetailsQuery : IRequest<AccountDto>
    {
        public int AccountId { get; set; }
    }
}
