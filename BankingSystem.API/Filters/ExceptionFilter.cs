using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BankingSystem.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var response = new
            {
                Error = context.Exception.Message,
                StackTrace = context.Exception.StackTrace
            };

            context.Result = new ObjectResult(response)
            {
                StatusCode = 500
            };
        }
    }
}
