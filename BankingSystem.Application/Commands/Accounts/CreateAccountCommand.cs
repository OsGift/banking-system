using BankingSystem.Application.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Application.Commands.Accounts
{
    public class CreateAccountCommand : IRequest<ResponseType<AccountDto>>
    {
        public string AccountHolderName { get; set; } = string.Empty;
        public decimal InitialDeposit { get; set; }
        public Guid UserId { get; set; }
    }
}