using BankingSystem.Application.DTOs;

namespace BankingSystem.Application.Interfaces
{
    public interface IAccountService
    {
        Task<bool> CreateAccount(string name, decimal initialDeposit);
        Task<AccountDto?> GetAccountDetails(Guid accountId);
    }
}
