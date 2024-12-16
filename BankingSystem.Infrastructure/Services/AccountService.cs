using AutoMapper;
using BankingSystem.Application.DTOs;
using BankingSystem.Application.Interfaces;
using BankingSystem.Domain.Entities;
using BankingSystem.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Logging;

namespace BankingSystem.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly AccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountService> _logger;

        // Injecting ILogger into the service
        public AccountService(AccountRepository accountRepository, IMapper mapper, ILogger<AccountService> logger)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // Logging only errors for account creation
        public async Task<AccountDto> CreateAccountAsync(Account account)
        {
            try
            {
                await _accountRepository.CreateAccountAsync(account);
                var accountDto = _mapper.Map<AccountDto>(account);
                return accountDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating account for account number: {AccountNumber}", account.AccountNumber);
                return null; 
            }
        }

        // Logging only errors for retrieving account by ID
        public async Task<AccountDto?> GetAccountByIdAsync(Guid accountId)
        {
            try
            {
                var account = await _accountRepository.GetAccountByIdAsync(accountId);
                if (account == null)
                {
                    return null;
                }

                return _mapper.Map<AccountDto>(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving account for account ID: {AccountId}", accountId);
                return null;
            }
        }

        // Logging only errors for retrieving account by account number
        public async Task<AccountDto?> GetAccountByAccountNumberAsync(string accountNumber)
        {
            try
            {
                var account = await _accountRepository.GetAccountByAccountNumberAsync(accountNumber);
                if (account == null)
                {
                    return null;
                }

                return _mapper.Map<AccountDto>(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving account for account number: {AccountNumber}", accountNumber);
                return null;
            }
        }

        // Logging only errors for retrieving all accounts
        public async Task<List<AccountDto>> GetAccounts()
        {
            try
            {
                var accounts = await _accountRepository.GetAccountsAsync();
                return _mapper.Map<List<AccountDto>>(accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all accounts");
                return new List<AccountDto>();
            }
        }

        // Logging only errors for retrieving accounts by user
        public async Task<List<AccountDto>> GetAccountsByUser(Guid userId)
        {
            try
            {
                var accounts = await _accountRepository.GetAccountsByUserAsync(userId);
                return _mapper.Map<List<AccountDto>>(accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving accounts for user ID: {UserId}", userId);
                return new List<AccountDto>();
            }
        }

        // Logging only errors for updating account
        public async Task<AccountDto> UpdateAccountAsync(Account account)
        {
            try
            {
                await _accountRepository.UpdateAccountAsync(account);
                return _mapper.Map<AccountDto>(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating account for account number: {AccountNumber}", account.AccountNumber);
                return null;
            }
        }
    }
}