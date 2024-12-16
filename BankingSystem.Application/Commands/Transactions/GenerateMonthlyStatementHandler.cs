using BankingSystem.Application.DTOs;
using BankingSystem.Application.Interfaces;
using MediatR;

namespace BankingSystem.Application.Commands.Transactions
{
    public class GenerateMonthlyStatementHandler : IRequestHandler<GenerateMonthlyStatementCommand, ResponseType<string>>
    {
        private readonly ITransactionService _transactionService;

        public GenerateMonthlyStatementHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<ResponseType<string>> Handle(GenerateMonthlyStatementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var statement = await _transactionService.GenerateMonthlyStatementAsync(request.AccountNumber, request.Month, request.Year);

                // Check if the statement is null or empty and return the appropriate response
                return string.IsNullOrEmpty(statement)
                    ? ResponseType<string>.Failure("Generated statement is null or empty.")
                    : ResponseType<string>.Success(statement, "Monthly statement generated successfully.");
            }
            catch (Exception ex)
            {
                return ResponseType<string>.Failure($"Error generating monthly statement: {ex.Message}");
            }
        }

    }
}
