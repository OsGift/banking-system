using BankingSystem.Infrastructure.Persistence.Repositories;
using MediatR;

namespace BankingSystem.Application.Commands.Transactions
{
    public class DepositHandler : IRequestHandler<DepositCommand, bool>
    {
        private readonly AccountRepository _repository;

        public DepositHandler(AccountRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            var account = _repository.GetById(request.AccountId);
            if (account != null)
            {
                account.Deposit(request.Amount);
                _repository.Update(account);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
