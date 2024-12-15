using BankingSystem.Application.DTOs.BankingSystem.Application.DTOs;

namespace BankingSystem.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<bool> DepositAsync(Guid accountId, decimal amount);
        Task<bool> WithdrawAsync(Guid accountId, decimal amount);
        Task<List<TransactionDto>> GetTransactionHistoryAsync(Guid accountId);
        Task<string> GenerateMonthlyStatementAsync(Guid accountId, int month, int year);
    }
}
