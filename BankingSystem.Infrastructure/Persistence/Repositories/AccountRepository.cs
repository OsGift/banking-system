using BankingSystem.Domain.Entities;
using BankingSystem.Infrastructure.Persistence.Contexts;

namespace BankingSystem.Infrastructure.Persistence.Repositories
{
    public class AccountRepository
    {
        private readonly BankingDbContext _context;

        public AccountRepository(BankingDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Account> GetAll() => _context.Accounts.ToList();

        public Account? GetById(Guid id) => _context.Accounts.FirstOrDefault(a => a.Id == id);

        public void Add(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
        }

        public void Update(Account account)
        {
            _context.Accounts.Update(account);
            _context.SaveChanges();
        }
    }
}
