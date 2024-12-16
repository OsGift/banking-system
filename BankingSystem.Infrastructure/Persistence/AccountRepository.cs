using BankingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace BankingSystem.Infrastructure.Persistence.Repositories
{
    public class AccountRepository
    {
        private readonly DatabaseContext _context;
        private readonly Random _random;

        public AccountRepository(DatabaseContext context)
        {
            _context = context;
            _random = new Random(); // For generating random account numbers
        }

        // Method to generate a random account number
        private string GenerateAccountNumber()
        {
            var firstPart = _random.Next(100000000, 999999999); 
            var secondPart = _random.Next(1000, 9999); 

            // Combine the two parts into a single 10-digit account number
            var accountNumber = firstPart.ToString() + secondPart.ToString();
            return accountNumber;
        }

        // Method to ensure the account number is unique
        private async Task<string> GenerateUniqueAccountNumberAsync()
        {
            string accountNumber;
            bool accountNumberExists;

            do
            {
                accountNumber = GenerateAccountNumber(); // Generate a new account number
                accountNumberExists = await AccountNumberExistsAsync(accountNumber); // Check if it exists in the database
            }
            while (accountNumberExists); // Repeat if the account number exists

            return accountNumber;
        }

        // Method to check if an account number exists in the database
        private async Task<bool> AccountNumberExistsAsync(string accountNumber)
        {
            var existingAccount = await _context.Accounts
                .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
            return existingAccount != null; // Returns true if account number exists
        }

        // Example method to create a new account
        public async Task<Account?> CreateAccountAsync(Account account)
        {
            // Generate a unique account number
            account.AccountNumber = await GenerateUniqueAccountNumberAsync();

            await _context.Accounts.AddAsync(account);
            var saved = await _context.SaveChangesAsync();
            if (saved > 0)
                return account;
            return null;
        }

        // Other repository methods (GetAccountByIdAsync, GetAccountByAccountNumberAsync, etc.)
        public async Task<Account?> GetAccountByIdAsync(Guid accountId)
        {
            return await _context.Accounts.Include(a => a.Transactions)
                                          .FirstOrDefaultAsync(a => a.AccountId == accountId);
        }

        public async Task<Account?> GetAccountByAccountNumberAsync(string accountNumber)
        {
            return await _context.Accounts.Include(a => a.Transactions)
                                          .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        }

        public async Task<List<Account>> GetAccountsByUserAsync(Guid accountNumber)
        {
            return await _context.Accounts.Include(a => a.Transactions)
                                          .Where(a => a.UserId == accountNumber).ToListAsync();
        }

        public async Task<List<Account>> GetAccountsAsync()
        {
            return await _context.Accounts.Include(a => a.Transactions).ToListAsync();
        }

        public async Task<Account> UpdateAccountAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
            return account;
        }
    }
}
