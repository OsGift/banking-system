namespace BankingSystem.Application.DTOs
{
    public class ResponseType<T>
    {
        public T Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
    }
}
