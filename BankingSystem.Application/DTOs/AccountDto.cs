namespace BankingSystem.Application.DTOs
{
    public class AccountDto
    {
        public int AccountId { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public string AccountHolderName { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }
}
