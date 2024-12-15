using BankingSystem.Application.DTOs;
using BankingSystem.Application.Interfaces;
using MediatR;

namespace BankingSystem.Application.Queries.Accounts
{
    public class GetAccountDetailsHandler : IRequestHandler<GetAccountDetailsQuery, AccountDto>
    {
        private readonly IAccountService _accountService;

        public GetAccountDetailsHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<AccountDto> Handle(GetAccountDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _accountService.GetAccountDetailsAsync(request.AccountId);
        }
    }
}