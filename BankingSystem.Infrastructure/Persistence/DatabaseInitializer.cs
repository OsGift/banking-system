using BankingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                    new User { UserId = Guid.NewGuid(), Username = "janesmith", Email = "janesmith@yopmail.com", PasswordHash = HashPassword("janeAdmin2024!"), Role = "Admin", CreatedAt = DateTime.Now.AddMonths(-1) },
                    new User { UserId = Guid.NewGuid(), Username = "michaeljohnson", Email = "michaeljohnson@yopmail.com", PasswordHash = HashPassword("mikeAdmin2024!"), Role = "Admin", CreatedAt = DateTime.Now.AddMonths(-2) },
                    new User { UserId = Guid.NewGuid(), Username = "susanbrown", Email = "susanbrown@yopmail.com", PasswordHash = HashPassword("susanAdmin2024!"), Role = "Admin", CreatedAt = DateTime.Now.AddMonths(-3) },
                    new User { UserId = Guid.NewGuid(), Username = "lindawhite", Email = "lindawhite@yopmail.com", PasswordHash = HashPassword("lindaManager2024!"), Role = "Manager", CreatedAt = DateTime.Now.AddMonths(-4) },
                    new User { UserId = Guid.NewGuid(), Username = "davidmiller", Email = "davidmiller@yopmail.com", PasswordHash = HashPassword("davidManager2024!"), Role = "Manager", CreatedAt = DateTime.Now.AddMonths(-5) },
                    new User { UserId = Guid.NewGuid(), Username = "chrisdavis", Email = "chrisdavis@yopmail.com", PasswordHash = HashPassword("chrisManager2024!"), Role = "Manager", CreatedAt = DateTime.Now.AddMonths(-6) },
                    new User { UserId = Guid.NewGuid(), Username = "emilywilson", Email = "emilywilson@yopmail.com", PasswordHash = HashPassword("emilyUser2024!"), Role = "User", CreatedAt = DateTime.Now.AddMonths(-7) },
                    new User { UserId = Guid.NewGuid(), Username = "robertmoore", Email = "robertmoore@yopmail.com", PasswordHash = HashPassword("robertUser2024!"), Role = "User", CreatedAt = DateTime.Now.AddMonths(-8) },
                    new User { UserId = Guid.NewGuid(), Username = "oliviajones", Email = "oliviajones@yopmail.com", PasswordHash = HashPassword("oliviaUser2024!"), Role = "User", CreatedAt = DateTime.Now.AddMonths(-9) }
                };

                await _context.Users.AddRangeAsync(users);
                await _context.SaveChangesAsync();
            }

            // Retrieve all users from the context after adding them
            var usersInDb = await _context.Users.ToListAsync();

            // Seed Accounts (Optional)
            if (!_context.Accounts.Any())
            {
                var accounts = new List<Account>
                {
                    new Account { AccountNumber = "123456789012", AccountHolderName = "John Doe", Balance = 5234.50m, UserId = usersInDb.FirstOrDefault(u => u.Username == "janesmith")?.UserId ?? Guid.Empty },
                    new Account { AccountNumber = "876543210987", AccountHolderName = "Jane Doe", Balance = 1327.75m, UserId = usersInDb.FirstOrDefault(u => u.Username == "michaeljohnson")?.UserId ?? Guid.Empty },
                    new Account { AccountNumber = "112233445566", AccountHolderName = "Mike Ross", Balance = 9020.00m, UserId = usersInDb.FirstOrDefault(u => u.Username == "susanbrown")?.UserId ?? Guid.Empty },
                    new Account { AccountNumber = "987654321234", AccountHolderName = "Emily Wilson", Balance = 50.00m, UserId = usersInDb.FirstOrDefault(u => u.Username == "emilywilson")?.UserId ?? Guid.Empty },
                    new Account { AccountNumber = "432198765432", AccountHolderName = "Robert Moore", Balance = 350.25m, UserId = usersInDb.FirstOrDefault(u => u.Username == "robertmoore")?.UserId ?? Guid.Empty },
                    new Account { AccountNumber = "246813579024", AccountHolderName = "Olivia Jones", Balance = 1742.90m, UserId = usersInDb.FirstOrDefault(u => u.Username == "oliviajones")?.UserId ?? Guid.Empty }
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
