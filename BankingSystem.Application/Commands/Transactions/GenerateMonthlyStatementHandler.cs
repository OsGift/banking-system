using BankingSystem.Application.Interfaces;
using MediatR;

namespace BankingSystem.Application.Commands.Transactions
{
    public class GenerateMonthlyStatementHandler : IRequestHandler<GenerateMonthlyStatementCommand, string>
    {
        private readonly ITransactionService _transactionService;

        public GenerateMonthlyStatementHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<string> Handle(GenerateMonthlyStatementCommand request, CancellationToken cancellationToken)
        {
            return await _transactionService.GenerateMonthlyStatementAsync(request.AccountId, request.Month, request.Year);
        }
    }
}
