using BankingSystem.Domain.Entities;
using BankingSystem.Infrastructure.Services;

namespace BankingSystem.Infrastructure.Persistence.Repositories
{
    public class CachedTransactionRepository : TransactionRepository
    {
        private readonly RedisCacheService _cacheService;

        public CachedTransactionRepository(DatabaseContext context, RedisCacheService cacheService) : base(context)
        {
            _cacheService = cacheService;
        }

        public async Task<List<Transaction>> GetCachedTransactionsByAccountIdAsync(int accountId)
        {
            var cacheKey = $"transactions_account_{accountId}";
            var cachedTransactions = await _cacheService.GetAsync<List<Transaction>>(cacheKey);

            if (cachedTransactions != null)
            {
                return cachedTransactions;
            }

            // If not in cache, fetch from DB and store in cache
            var transactions = await GetTransactionsByAccountIdAsync(accountId);
            await _cacheService.SetAsync(cacheKey, transactions);

            return transactions;
        }

        public async Task AddTransactionAndInvalidateCacheAsync(Transaction transaction)
        {
            await AddTransactionAsync(transaction);
            var cacheKey = $"transactions_account_{transaction.AccountId}";
            await _cacheService.RemoveAsync(cacheKey); // Invalidate cache
        }
    }
}
