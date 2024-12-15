using BankingSystem.Application.Interfaces;
using BankingSystem.Domain.Entities;
using MediatR;

namespace BankingSystem.Application.Commands.Accounts
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, bool>
    {
        private readonly IAccountService _accountService;

        public CreateAccountHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<bool> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = new Account
            {
                AccountHolderName = request.AccountHolderName,
                Balance = request.InitialDeposit,
                UserId = request.UserId,
            };

            return await _accountService.CreateAccountAsync(account);
        }
    }
}
