
using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Domain.Entities
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public string AccountHolderName { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
