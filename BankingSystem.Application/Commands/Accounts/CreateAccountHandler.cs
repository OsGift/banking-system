using BankingSystem.Domain.Entities;
using BankingSystem.Infrastructure.Persistence.Repositories;
using MediatR;

namespace BankingSystem.Application.Commands.Accounts
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, bool>
    {
        private readonly AccountRepository _repository;

        public CreateAccountHandler(AccountRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = new Account { Name = request.Name };
            account.Deposit(request.InitialDeposit);
            _repository.Add(account);
            return Task.FromResult(true);
        }
    }
}
