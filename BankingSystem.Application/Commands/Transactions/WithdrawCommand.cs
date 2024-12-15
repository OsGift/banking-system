using MediatR;

namespace BankingSystem.Application.Commands.Transactions
{
    public class WithdrawCommand : IRequest<bool>
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
