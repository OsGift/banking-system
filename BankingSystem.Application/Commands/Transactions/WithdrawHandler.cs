using BankingSystem.Application.DTOs;
using BankingSystem.Application.Interfaces;
using MediatR;
using Serilog;

namespace BankingSystem.Application.Commands.Transactions
{
    public class WithdrawHandler : IRequestHandler<WithdrawCommand, ResponseType<bool>>
    {
        private readonly ITransactionService _transactionService;

        public WithdrawHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<ResponseType<bool>> Handle(WithdrawCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Perform the withdrawal operation
                var success = await _transactionService.WithdrawAsync(request.AccountNumber, request.Amount);

                // Check if the withdrawal was successful and return the appropriate response
                if (success)
                {
                    return ResponseType<bool>.Success(true, "Withdrawal successful.");
                }
                else
                {
                    Log.Warning("Withdrawal failed for AccountNumber: {AccountNumber}. Insufficient funds or invalid operation.", request.AccountNumber);
                    return ResponseType<bool>.Failure("Withdrawal failed due to insufficient funds or an invalid operation.");
                }
            }
            catch (Exception ex)
            {
                // Log the error if an exception occurs
                Log.Error(ex, "Error occurred while attempting to withdraw {Amount} from AccountNumber: {AccountNumber}", request.Amount, request.AccountNumber);
                return ResponseType<bool>.Failure($"Error occurred while processing the withdrawal: {ex.Message}");
            }
        }
    }
}
