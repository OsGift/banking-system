using BankingSystem.Application.DTOs;
using BankingSystem.Domain.Entities;

namespace BankingSystem.Application.Interfaces
{
    public interface IAccountService
    {
        Task<bool> CreateAccountAsync(Account account);
        Task<AccountDto?> GetAccountDetailsAsync(Guid accountId);
    }
}