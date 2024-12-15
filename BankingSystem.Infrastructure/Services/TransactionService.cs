using BankingSystem.Application.DTOs.BankingSystem.Application.DTOs;
using BankingSystem.Application.Interfaces;
using BankingSystem.Domain.Entities;
using BankingSystem.Domain.Enums;
using BankingSystem.Infrastructure.Persistence.Repositories;

namespace BankingSystem.Infrastructure.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly AccountRepository _accountRepository;
        private readonly TransactionRepository _transactionRepository;
        private readonly RedisCacheService _cacheService;

        public TransactionService(
            AccountRepository accountRepository,
            TransactionRepository transactionRepository,
            RedisCacheService cacheService)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _cacheService = cacheService;
        }

        public async Task<bool> DepositAsync(Guid accountId, decimal amount)
        {
            var account = await _accountRepository.GetAccountByIdAsync(accountId);
            if (account == null)
                return false;

            account.Balance += amount;
            await _accountRepository.UpdateAccountAsync(account);

            var transaction = new Transaction
            {
                AccountId = accountId,
                Amount = amount,
                TransactionDate = DateTime.UtcNow,
                Description = "Deposit",
                Type = TransactionType.Deposit
            };

            await _transactionRepository.AddTransactionAsync(transaction);

            // Invalidate cached transaction history for this account
            var cacheKey = $"transactions_account_{accountId}";
            await _cacheService.RemoveAsync(cacheKey);

            return true;
        }

        public async Task<bool> WithdrawAsync(Guid accountId, decimal amount)
        {
            var account = await _accountRepository.GetAccountByIdAsync(accountId);
            if (account == null || account.Balance < amount)
                return false;

            account.Balance -= amount;
            await _accountRepository.UpdateAccountAsync(account);

            var transaction = new Transaction
            {
                AccountId = accountId,
                Amount = -amount,
                TransactionDate = DateTime.UtcNow,
                Description = "Withdrawal",
                Type = TransactionType.Withdrawal
            };

            await _transactionRepository.AddTransactionAsync(transaction);

            // Invalidate cached transaction history for this account
            var cacheKey = $"transactions_account_{accountId}";
            await _cacheService.RemoveAsync(cacheKey);

            return true;
        }

        public async Task<List<TransactionDto>> GetTransactionHistoryAsync(Guid accountId)
        {
            var cacheKey = $"transactions_account_{accountId}";

            // Attempt to get cached data
            var cachedTransactions = await _cacheService.GetAsync<List<Transaction>>(cacheKey);
            if (cachedTransactions != null)
            {
                return cachedTransactions.Select(t => new TransactionDto
                {
                    TransactionId = t.TransactionId,
                    Amount = t.Amount,
                    TransactionDate = t.TransactionDate,
                    Description = t.Description
                }).ToList();
            }

            // Fetch from DB if not cached
            var transactions = await _transactionRepository.GetTransactionsByAccountIdAsync(accountId);

            // Cache the result for future use
            await _cacheService.SetAsync(cacheKey, transactions);

            return transactions.Select(t => new TransactionDto
            {
                TransactionId = t.TransactionId,
                Amount = t.Amount,
                TransactionDate = t.TransactionDate,
                Description = t.Description
            }).ToList();
        }

        public async Task<string> GenerateMonthlyStatementAsync(Guid accountId, int month, int year)
        {
            var transactions = await _transactionRepository.GetTransactionsByAccountIdAsync(accountId);
            var filteredTransactions = transactions
                .Where(t => t.TransactionDate.Month == month && t.TransactionDate.Year == year)
                .ToList();

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
    }
}