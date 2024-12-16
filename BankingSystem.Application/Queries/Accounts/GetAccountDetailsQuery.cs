using BankingSystem.Application.DTOs;
using MediatR;

namespace BankingSystem.Application.Queries.Accounts
{
    public class GetAccountDetailsQuery : IRequest<ResponseType<AccountDto>>
    {
        public string AccountNumber { get; set; }
    }
}