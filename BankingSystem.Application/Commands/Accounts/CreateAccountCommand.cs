using MediatR;

namespace BankingSystem.Application.Commands.Accounts
{
    public class CreateAccountCommand : IRequest<bool>
    {
        public string AccountHolderName { get; set; } = string.Empty;
        public decimal InitialDeposit { get; set; }
        public Guid UserId { get; set; }
    }
}