

namespace BankingSystem.Domain.Exceptions
{
    public class UnauthorizedAccessException : Exception
    {
        public UnauthorizedAccessException(string message) : base(message) { }
    }
}
