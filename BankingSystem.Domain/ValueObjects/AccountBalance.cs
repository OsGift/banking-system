namespace BankingSystem.Domain.ValueObjects
{
    public class AccountBalance
    {
        public decimal Value { get; private set; }

        public AccountBalance(decimal initialBalance)
        {
            if (initialBalance >= 0)
            {
                Value = initialBalance;
            }
        }

        public void Add(decimal amount)
        {
            if (amount >= 0)
            {
                Value += amount;
            }
        }

        public void Subtract(decimal amount)
        {
            if (amount >= 0 && Value >= amount)
            {
                Value -= amount;
            }
        }
    }
}
