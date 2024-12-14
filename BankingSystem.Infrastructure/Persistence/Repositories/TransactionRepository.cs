using BankingSystem.Domain.Entities;
using BankingSystem.Infrastructure.Persistence.Contexts;


namespace BankingSystem.Infrastructure.Persistence.Repositories
{
    public class TransactionRepository
    {
        private readonly BankingDbContext _context;

        public TransactionRepository(BankingDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Transaction> GetAllByAccountId(Guid accountId) =>
            _context.Transactions.Where(t => t.AccountId == accountId).ToList();

        public void Add(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }
    }
}
