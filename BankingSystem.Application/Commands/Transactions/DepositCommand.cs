using BankingSystem.Application.DTOs;
using MediatR;

namespace BankingSystem.Application.Commands.Transactions
{
    public class DepositCommand : IRequest<ResponseType<bool>>
    {
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}