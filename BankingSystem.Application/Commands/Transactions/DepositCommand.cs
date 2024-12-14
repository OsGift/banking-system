using MediatR;

namespace BankingSystem.Application.Commands.Transactions
{
    public class DepositCommand : IRequest<bool>
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
