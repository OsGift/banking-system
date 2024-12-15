using BankingSystem.Application.Interfaces;
using MediatR;

namespace BankingSystem.Application.Commands.Transactions
{
    public class DepositHandler : IRequestHandler<DepositCommand, bool>
    {
        private readonly ITransactionService _transactionService;

        public DepositHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<bool> Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            return await _transactionService.DepositAsync(request.AccountId, request.Amount);
        }
    }
}
