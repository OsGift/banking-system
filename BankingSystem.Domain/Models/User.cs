namespace BankingSystem.Domain.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // Admin, Customer
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<Account> Accounts { get; set; } = new List<Account>(); // User's accounts
    }
}
