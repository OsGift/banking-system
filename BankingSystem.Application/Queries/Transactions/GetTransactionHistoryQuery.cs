using BankingSystem.Application.DTOs;
using BankingSystem.Application.DTOs.BankingSystem.Application.DTOs;
using MediatR;

namespace BankingSystem.Application.Queries.Transactions
{
    public class GetTransactionHistoryQuery : IRequest<ResponseType<List<TransactionDto>>>
    {
        public string AccountNumber { get; set; }
    }

}

