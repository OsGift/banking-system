using BankingSystem.Application.DTOs;
using BankingSystem.Domain.Entities;

namespace BankingSystem.Application.Interfaces
{
    public interface IAccountService
    {
        Task<AccountDto> CreateAccountAsync(Account account);
        Task<AccountDto?> GetAccountByIdAsync(Guid accountId);
        Task<AccountDto?> GetAccountByAccountNumberAsync(string accountNumber);
        Task<List<AccountDto>> GetAccounts();
        Task<List<AccountDto>> GetAccountsByUser(Guid accountNumber);
        Task<AccountDto> UpdateAccountAsync(Account account);
    }
}