using BankingSystem.Application.DTOs;
using BankingSystem.Infrastructure.Persistence.Repositories;
using MediatR;

namespace BankingSystem.Application.Queries.Transactions
{
    public class GetTransactionHistoryHandler : IRequestHandler<GetTransactionHistoryQuery, IEnumerable<TransactionDto>>
    {
        private readonly TransactionRepository _repository;

        public GetTransactionHistoryHandler(TransactionRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<TransactionDto>> Handle(GetTransactionHistoryQuery request, CancellationToken cancellationToken)
        {
            var transactions = _repository.GetAllByAccountId(request.AccountId);
            return Task.FromResult(transactions.Select(t => new TransactionDto
            {
                Id = t.Id,
                Amount = t.Amount,
                Type = t.Type.ToString(),
                Timestamp = t.Timestamp
            }));
        }
    }
}
