using BankingSystem.Application.DTOs;
using MediatR;

namespace BankingSystem.Application.Queries.Transactions
{
    public class GetTransactionHistoryQuery : IRequest<IEnumerable<TransactionDto>>
    {
        public Guid AccountId { get; set; }
    }
}
