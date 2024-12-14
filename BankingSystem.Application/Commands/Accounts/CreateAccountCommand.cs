using MediatR;

namespace BankingSystem.Application.Commands.Accounts
{
    public class CreateAccountCommand : IRequest<bool>
    {
        public string Name { get; set; } = string.Empty;
        public decimal InitialDeposit { get; set; }
    }
}
