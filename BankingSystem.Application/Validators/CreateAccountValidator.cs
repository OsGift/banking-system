using BankingSystem.Application.Commands.Accounts;
using FluentValidation;

namespace BankingSystem.Application.Validators
{
    public class CreateAccountValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountValidator()
        {
            RuleFor(x => x.AccountHolderName).NotEmpty().WithMessage("Account holder name is required.");
            RuleFor(x => x.InitialDeposit).GreaterThanOrEqualTo(0).WithMessage("Initial deposit must be non-negative.");
        }
    }
}
