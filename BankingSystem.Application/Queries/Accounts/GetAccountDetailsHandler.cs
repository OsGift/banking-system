using BankingSystem.Application.DTOs;
using BankingSystem.Application.Interfaces;
using MediatR;

namespace BankingSystem.Application.Queries.Accounts
{
    public class GetAccountDetailsHandler : IRequestHandler<GetAccountDetailsQuery, ResponseType<AccountDto>>
    {
        private readonly IAccountService _accountService;

        public GetAccountDetailsHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<ResponseType<AccountDto>> Handle(GetAccountDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Attempt to fetch account details asynchronously
                var accountDetails = await _accountService.GetAccountByAccountNumberAsync(request.AccountNumber);

                // If account details are found, return a success response
                if (accountDetails != null)
                {
                    return ResponseType<AccountDto>.Success(accountDetails);
                }
                else
                {
                    // If account details are not found, return a failure response
                    return ResponseType<AccountDto>.Failure("Account not found.");
                }
            }
            catch (Exception ex)
            {
                // If an error occurs, return a failure response with exception details
                return ResponseType<AccountDto>.Failure($"An error occurred: {ex.Message}");
            }
        }
    }
}