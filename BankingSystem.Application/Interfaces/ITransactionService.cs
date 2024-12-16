using BankingSystem.Application.DTOs.BankingSystem.Application.DTOs;

namespace BankingSystem.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<bool> DepositAsync(string accountNumber, decimal amount);
        Task<bool> WithdrawAsync(string accountNumber, decimal amount);
        Task<List<TransactionDto>> GetTransactionHistoryAsync(string accountNumber);
        Task<string?> GenerateMonthlyStatementAsync(string accountNumber, int month, int year);
    }
}
