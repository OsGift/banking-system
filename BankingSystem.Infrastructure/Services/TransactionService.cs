using AutoMapper;
using BankingSystem.Application.DTOs;
using BankingSystem.Application.DTOs.BankingSystem.Application.DTOs;
using BankingSystem.Application.Interfaces;
using BankingSystem.Domain.Entities;
using BankingSystem.Domain.Enums;
using BankingSystem.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Logging;

namespace BankingSystem.Infrastructure.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly AccountRepository _accountRepository;
        private readonly TransactionRepository _transactionRepository;
        private readonly RedisCacheService _cacheService;
        private readonly ILogger<TransactionService> _logger;
        private readonly IMapper _mapper;

        public TransactionService(
            AccountRepository accountRepository,
            TransactionRepository transactionRepository,
            RedisCacheService cacheService,
            ILogger<TransactionService> logger,
            IMapper mapper)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _cacheService = cacheService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<bool> DepositAsync(string accountNumber, decimal amount)
        {
            try
            {
                var account = await _accountRepository.GetAccountByAccountNumberAsync(accountNumber);
                if (account == null)
                    return false;

                account.Balance += amount;
                await _accountRepository.UpdateAccountAsync(account);

                var transaction = new Transaction
                {
                    AccountId = account.AccountId,
                    Amount = amount,
                    TransactionDate = DateTime.UtcNow,
                    Description = "Deposit",
                    Type = TransactionType.Deposit
                };

                await _transactionRepository.AddTransactionAsync(transaction);

                // Invalidate cached transaction history for this account
                var cacheKey = $"transactions_account_{account.AccountId}";
                await _cacheService.RemoveAsync(cacheKey);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing deposit for account number {AccountNumber}.", accountNumber);
                return false;
            }
        }

        public async Task<bool> WithdrawAsync(string accountNumber, decimal amount)
        {
            try
            {
                var account = await _accountRepository.GetAccountByAccountNumberAsync(accountNumber);
                if (account == null || account.Balance < amount)
                    return false;

                account.Balance -= amount;
                await _accountRepository.UpdateAccountAsync(account);

                var transaction = new Transaction
                {
                    AccountId = account.AccountId,
                    Amount = -amount,
                    TransactionDate = DateTime.UtcNow,
                    Description = "Withdrawal",
                    Type = TransactionType.Withdrawal
                };

                await _transactionRepository.AddTransactionAsync(transaction);

                // Invalidate cached transaction history for this account
                var cacheKey = $"transactions_account_{account.AccountId}";
                await _cacheService.RemoveAsync(cacheKey);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing withdrawal for account number {AccountNumber}.", accountNumber);
                return false;
            }
        }

        public async Task<List<TransactionDto>> GetTransactionHistoryAsync(string accountNumber)
        {
            try
            {
                var account = await _accountRepository.GetAccountByAccountNumberAsync(accountNumber);
                if (account == null)
                    return new List<TransactionDto>();

                var cacheKey = $"transactions_account_{account.AccountId}";

                // Attempt to get cached data
                var cachedTransactions = await _cacheService.GetAsync<List<Transaction>>(cacheKey);
                if (cachedTransactions != null)
                {
                    return _mapper.Map<List<TransactionDto>>(cachedTransactions);
                }

                // Fetch from DB if not cached
                var transactions = await _transactionRepository.GetTransactionsByAccountIdAsync(account.AccountId);

                // Cache the result for future use
                await _cacheService.SetAsync(cacheKey, transactions);

                return _mapper.Map<List<TransactionDto>>(transactions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching transaction history for account number {AccountNumber}.", accountNumber);
                return new List<TransactionDto>();
            }
        }

        public async Task<string?> GenerateMonthlyStatementAsync(string accountNumber, int month, int year)
        {
            try
            {
                var account = await _accountRepository.GetAccountByAccountNumberAsync(accountNumber);
                if (account == null)
                    return null;

                var transactions = await _transactionRepository.GetTransactionsByAccountIdAsync(account.AccountId);
                var filteredTransactions = transactions
                    .Where(t => t.TransactionDate.Month == month && t.TransactionDate.Year == year)
                    .ToList();

                if (!filteredTransactions.Any())
                    return null;

                var statement = $"Monthly Statement for {month}/{year}\n";
                statement += "---------------------------------\n";

                foreach (var transaction in filteredTransactions)
                {
                    statement += $"{transaction.TransactionDate}: {transaction.Description} - {transaction.Amount:C}\n";
                }

                statement += "---------------------------------\n";
                var total = filteredTransactions.Sum(t => t.Amount);
                statement += $"Total: {total:C}\n";

                return statement;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while generating monthly statement for account number {AccountNumber} for {Month}/{Year}.", accountNumber, month, year);
                return null;
            }
        }
    }
}
