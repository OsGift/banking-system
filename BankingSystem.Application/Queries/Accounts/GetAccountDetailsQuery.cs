using BankingSystem.Application.DTOs;
using MediatR;

namespace BankingSystem.Application.Queries.Accounts
{
    public class GetAccountDetailsQuery : IRequest<AccountDto>
    {
        public Guid AccountId { get; set; }
    }
}
