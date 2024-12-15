using BankingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Infrastructure.Persistence
{
    public class DatabaseInitializer
    {
        private readonly DatabaseContext _context;

        public DatabaseInitializer(DatabaseContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            // Ensure the database is created
            await _context.Database.MigrateAsync();

            // Seed Users
            if (!_context.Users.Any())
            {
                var users = new List<User>
                {
                    // Admins
                    new User { Username = "admin1", PasswordHash = HashPassword("password123"), Role = "Admin" },
                    new User { Username = "admin2", PasswordHash = HashPassword("password123"), Role = "Admin" },
                    new User { Username = "admin3", PasswordHash = HashPassword("password123"), Role = "Admin" },

                    // Managers
                    new User { Username = "manager1", PasswordHash = HashPassword("password123"), Role = "Manager" },
                    new User { Username = "manager2", PasswordHash = HashPassword("password123"), Role = "Manager" },
                    new User { Username = "manager3", PasswordHash = HashPassword("password123"), Role = "Manager" },

                    // Users
                    new User { Username = "user1", PasswordHash = HashPassword("password123"), Role = "User" },
                    new User { Username = "user2", PasswordHash = HashPassword("password123"), Role = "User" },
                    new User { Username = "user3", PasswordHash = HashPassword("password123"), Role = "User" },
                };

                await _context.Users.AddRangeAsync(users);
                await _context.SaveChangesAsync();
            }

            // Seed Accounts (Optional)
            if (!_context.Accounts.Any())
            {
                var accounts = new List<Account>
                {
                    new Account { AccountNumber = "12345678", AccountHolderName = "John Doe", Balance = 1000 },
                    new Account { AccountNumber = "87654321", AccountHolderName = "Jane Doe", Balance = 500 },
                    new Account { AccountNumber = "11223344", AccountHolderName = "Mike Ross", Balance = 750 }
                };

                await _context.Accounts.AddRangeAsync(accounts);
                await _context.SaveChangesAsync();
            }
        }

        private string HashPassword(string password)
        {
            // Example hash logic (use a proper hashing library in production, e.g., BCrypt)
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}
