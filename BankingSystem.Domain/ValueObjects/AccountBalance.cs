namespace BankingSystem.Domain.ValueObjects
{
    public class AccountBalance
    {
        public decimal Balance { get; private set; }

        public AccountBalance(decimal initialBalance)
        {
            if (initialBalance < 0)
                throw new ArgumentException("Initial balance cannot be negative.");
            Balance = initialBalance;
        }

        public void Credit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");
            Balance += amount;
        }

        public void Debit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");
            if (amount > Balance)
                throw new InvalidOperationException("Insufficient balance.");
            Balance -= amount;
        }

        public override string ToString()
        {
            return $"${Balance:F2}";
        }
    }
}
