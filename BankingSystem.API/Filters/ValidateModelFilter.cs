// API/Controllers/AccountsController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BankingSystem.API.Filters
{
    public class SwaggerConfiguration : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
