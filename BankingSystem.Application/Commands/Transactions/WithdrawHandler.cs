using BankingSystem.Application.Interfaces;
using MediatR;


namespace BankingSystem.Application.Commands.Transactions
{
    public class WithdrawHandler : IRequestHandler<WithdrawCommand, bool>
    {
        private readonly ITransactionService _transactionService;

        public WithdrawHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<bool> Handle(WithdrawCommand request, CancellationToken cancellationToken)
        {
            return await _transactionService.WithdrawAsync(request.AccountId, request.Amount);
        }
    }
}
