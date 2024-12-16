using BankingSystem.Application.DTOs;
using BankingSystem.Application.Interfaces;
using BankingSystem.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging; 
namespace BankingSystem.Application.Commands.Accounts
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, ResponseType<AccountDto>>
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<CreateAccountHandler> _logger;

        public CreateAccountHandler(IAccountService accountService, ILogger<CreateAccountHandler> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        public async Task<ResponseType<AccountDto>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            // Validate InitialDeposit
            if (request.InitialDeposit <= 0)
            {
                var errorMessage = "Initial deposit must be greater than zero.";
                _logger.LogWarning(errorMessage); // Log a warning if validation fails
                return ResponseType<AccountDto>.Failure(errorMessage);
            }

            // Create Account entity
            var account = new Account
            {
                AccountHolderName = request.AccountHolderName,
                Balance = request.InitialDeposit,
                UserId = request.UserId,
            };

            try
            {
                // Call service to create the account
                var accountDto = await _accountService.CreateAccountAsync(account);

                if (accountDto != null)
                {
                    // Log success message with structured data
                    _logger.LogInformation("Account created successfully for UserId: {UserId}, AccountHolderName: {AccountHolderName}",
                        request.UserId, request.AccountHolderName);

                    return ResponseType<AccountDto>.Success(accountDto, "Account created successfully.");
                }
                else
                {
                    // Log failure message if account creation fails
                    _logger.LogWarning("Account creation failed for UserId: {UserId}, AccountHolderName: {AccountHolderName}",
                        request.UserId, request.AccountHolderName);

                    return ResponseType<AccountDto>.Failure("Account creation failed.");
                }
            }
            catch (Exception ex)
            {
                // Log unexpected errors with exception details
                _logger.LogError(ex, "An error occurred while creating an account for UserId: {UserId}, AccountHolderName: {AccountHolderName}",
                    request.UserId, request.AccountHolderName);

                return ResponseType<AccountDto>.Failure("An error occurred while creating the account.");
            }
        }
    }
}