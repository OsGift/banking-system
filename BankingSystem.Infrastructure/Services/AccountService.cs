using BankingSystem.Application.DTOs;
using BankingSystem.Application.Interfaces;
using BankingSystem.Domain.Entities;
using BankingSystem.Infrastructure.Persistence.Repositories;

namespace BankingSystem.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly AccountRepository _accountRepository;

        public AccountService(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<bool> CreateAccountAsync(Account account)
        {
            try
            {
                await _accountRepository.CreateAccountAsync(account);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<AccountDto?> GetAccountDetailsAsync(int accountId)
        {
            var account = await _accountRepository.GetAccountByIdAsync(accountId);
            if (account == null)
                return null;

            return new AccountDto
            {
                AccountId = account.AccountId,
                AccountNumber = account.AccountNumber,
                AccountHolderName = account.AccountHolderName,
                Balance = account.Balance
            };
        }
    }
}
