using BankingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Infrastructure.Persistence.Repositories
{
    public class AccountRepository
    {
        private readonly DatabaseContext _context;

        public AccountRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Account?> GetAccountByIdAsync(int accountId)
        {
            return await _context.Accounts.Include(a => a.Transactions)
                                          .FirstOrDefaultAsync(a => a.AccountId == accountId);
        }

        public async Task CreateAccountAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAccountAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }
    }
}