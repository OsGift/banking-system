namespace BankingSystem.Application.DTOs
{
    namespace BankingSystem.Application.DTOs
    {
        public class TransactionDto
        {
            public int TransactionId { get; set; }
            public decimal Amount { get; set; }
            public DateTime TransactionDate { get; set; }
            public string Description { get; set; } = string.Empty;
        }
    }

}
