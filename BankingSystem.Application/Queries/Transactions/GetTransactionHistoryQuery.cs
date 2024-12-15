using BankingSystem.Application.DTOs.BankingSystem.Application.DTOs;
using MediatR;

namespace BankingSystem.Application.Queries.Accounts
{

    namespace BankingSystem.Application.Queries.Transactions
    {
        public class GetTransactionHistoryQuery : IRequest<List<TransactionDto>>
        {
            public int AccountId { get; set; }
        }
    }

}
