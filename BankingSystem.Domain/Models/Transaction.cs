using BankingSystem.Domain.Enums;

namespace BankingSystem.Domain.Entities
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; } = string.Empty;

        public TransactionType Type { get; set; }

        public Account Account { get; set; }
    }
}
