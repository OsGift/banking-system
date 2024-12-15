using BankingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Infrastructure
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure models
            modelBuilder.Entity<Account>().HasKey(a => a.AccountId);
            modelBuilder.Entity<Transaction>().HasKey(t => t.TransactionId);
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
        }
    }
}
