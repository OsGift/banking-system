namespace BankingSystem.Application.DTOs
{
    public class ResponseType<T>
    {
        public T Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }

        // Static method to create a success response with data
        public static ResponseType<T> Success(T data, string message = "Operation successful")
        {
            return new ResponseType<T>
            {
                Data = data,
                IsSuccess = true,
                Message = message
            };
        }

        // Static method to create a failure response with a message
        public static ResponseType<T> Failure(string message)
        {
            return new ResponseType<T>
            {
                Data = default(T), // No data in case of failure
                IsSuccess = false,
                Message = message
            };
        }

        // Static method to create an empty success response (no data)
        public static ResponseType<T> Success(string message = "Operation successful")
        {
            return new ResponseType<T>
            {
                Data = default(T),
                IsSuccess = true,
                Message = message
            };
        }

        // Static method to create an empty failure response (no data)
        public static ResponseType<T> Failure()
        {
            return new ResponseType<T>
            {
                Data = default(T),
                IsSuccess = false,
                Message = "Operation failed"
            };
        }

        // Method to update the response message
        public ResponseType<T> WithMessage(string message)
        {
            this.Message = message;
            return this;
        }
    }

}
