using BankingSystem.Application.DTOs;
using MediatR;


namespace BankingSystem.Application.Commands.Transactions
{
    using System.ComponentModel.DataAnnotations;

    public class GenerateMonthlyStatementCommand : IRequest<ResponseType<string>>
    {
        [Required(ErrorMessage = "Account number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Account number must be exactly 10 digits.")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "Month is required.")]
        [Range(1, 12, ErrorMessage = "Month must be between 1 and 12.")]
        public int Month { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Year must be a 4-digit number.")]
        public int Year { get; set; }
    }

}
