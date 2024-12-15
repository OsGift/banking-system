using BankingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Infrastructure.Persistence.Repositories
{
    public class TransactionRepository
    {
        private readonly DatabaseContext _context;

        public TransactionRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetTransactionsByAccountIdAsync(Guid accountId)
        {
            return await _context.Transactions
                                 .Where(t => t.AccountId == accountId)
                                 .OrderByDescending(t => t.TransactionDate)
                                 .ToListAsync();
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }
    }
}
