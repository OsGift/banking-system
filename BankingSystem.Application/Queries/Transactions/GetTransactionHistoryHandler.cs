using BankingSystem.Application.DTOs;
using BankingSystem.Application.DTOs.BankingSystem.Application.DTOs;
using BankingSystem.Application.Interfaces;
using MediatR;

namespace BankingSystem.Application.Queries.Transactions
{
    public class GetTransactionHistoryHandler : IRequestHandler<GetTransactionHistoryQuery, ResponseType<List<TransactionDto>>>
    {
        private readonly ITransactionService _transactionService;

        public GetTransactionHistoryHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<ResponseType<List<TransactionDto>>> Handle(GetTransactionHistoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var transactions = await _transactionService.GetTransactionHistoryAsync(request.AccountNumber);

                // Check if there are no transactions and return an appropriate response
                if (transactions == null || !transactions.Any())
                {
                    return ResponseType<List<TransactionDto>>.Failure("No transactions found for this account.");
                }

                return ResponseType<List<TransactionDto>>.Success(transactions, "Transaction history retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Log the error if something goes wrong (use Serilog for logging in real application)
                // Log.Error(ex, "Error occurred while fetching transaction history for AccountId: {AccountId}", request.AccountId);

                return ResponseType<List<TransactionDto>>.Failure($"Error retrieving transaction history: {ex.Message}");
            }
        }
    }
}