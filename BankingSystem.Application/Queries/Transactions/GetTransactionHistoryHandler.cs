using BankingSystem.Application.DTOs.BankingSystem.Application.DTOs;
using BankingSystem.Application.Interfaces;
using MediatR;

namespace BankingSystem.Application.Queries.Accounts
{

    namespace BankingSystem.Application.Queries.Transactions
    {
        public class GetTransactionHistoryHandler : IRequestHandler<GetTransactionHistoryQuery, List<TransactionDto>>
        {
            private readonly ITransactionService _transactionService;

            public GetTransactionHistoryHandler(ITransactionService transactionService)
            {
                _transactionService = transactionService;
            }

            public async Task<List<TransactionDto>> Handle(GetTransactionHistoryQuery request, CancellationToken cancellationToken)
            {
                return await _transactionService.GetTransactionHistoryAsync(request.AccountId);
            }
        }
    }

}
