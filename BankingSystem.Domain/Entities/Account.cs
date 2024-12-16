
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankingSystem.Domain.Entities
{
    public class Account
    {
        [Key]
        public Guid AccountId { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public string AccountHolderName { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public Guid UserId { get; set; }

        [JsonIgnore] // Prevent cyclical serialization
        public User? User { get; set; }

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
