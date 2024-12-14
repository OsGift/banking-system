namespace BankingSystem.Application.Interfaces
{
    public interface IPaymentGatewayService
    {
        bool ProcessPayment(string accountNumber, decimal amount);
    }
}
