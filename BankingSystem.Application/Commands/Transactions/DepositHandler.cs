using BankingSystem.Application.DTOs;
using BankingSystem.Application.Interfaces;
using MediatR;

namespace BankingSystem.Application.Commands.Transactions
{
    public class DepositHandler : IRequestHandler<DepositCommand, ResponseType<bool>>
    {
        private readonly ITransactionService _transactionService;

        public DepositHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<ResponseType<bool>> Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            return await _transactionService.DepositAsync(request.AccountNumber, request.Amount)
                ? ResponseType<bool>.Success(true, "Deposit successful.")
                : ResponseType<bool>.Failure("Deposit failed.");
        }

    }
}
