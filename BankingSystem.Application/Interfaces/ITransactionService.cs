using BankingSystem.Application.DTOs.BankingSystem.Application.DTOs;

namespace BankingSystem.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<bool> DepositAsync(int accountId, decimal amount);
        Task<bool> WithdrawAsync(int accountId, decimal amount);
        Task<List<TransactionDto>> GetTransactionHistoryAsync(int accountId);
        Task<string> GenerateMonthlyStatementAsync(int accountId, int month, int year);
    }
}
