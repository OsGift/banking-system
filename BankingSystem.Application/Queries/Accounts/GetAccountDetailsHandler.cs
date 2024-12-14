using BankingSystem.Application.DTOs;
using BankingSystem.Infrastructure.Persistence.Repositories;
using MediatR;

namespace BankingSystem.Application.Queries.Accounts
{
    public class GetAccountDetailsHandler : IRequestHandler<GetAccountDetailsQuery, AccountDto>
    {
        private readonly AccountRepository _repository;

        public GetAccountDetailsHandler(AccountRepository repository)
        {
            _repository = repository;
        }

        public Task<AccountDto> Handle(GetAccountDetailsQuery request, CancellationToken cancellationToken)
        {
            var account = _repository.GetById(request.AccountId);
            if (account != null)
            {
                return Task.FromResult(new AccountDto
                {
                    Id = account.Id,
                    Name = account.Name,
                    Balance = account.Balance
                });
            }
            return Task.FromResult<AccountDto>(null);
        }
    }
}
