using MediatR;


namespace BankingSystem.Application.Commands.Transactions
{
    public class GenerateMonthlyStatementCommand : IRequest<string>
    {
        public Guid AccountId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
