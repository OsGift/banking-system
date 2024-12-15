﻿using MediatR;

namespace BankingSystem.Application.Commands.Transactions
{
    public class DepositCommand : IRequest<bool>
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}