using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.WebUtilities;

namespace Desafio.Consumer.Services.Filters
{
    public class MVCErrorFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new ObjectResult(context.Exception.Message) // isso ou uma mensagem padrão
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
            var statusCode = context.HttpContext.Response.StatusCode;
            context.ExceptionHandled = true;
            context.Result = new RedirectToActionResult("Error", "Home", new { Message = context.Exception.Message, StatusCode = statusCode});
            /*
            context.Result = new ViewResult{
                ViewName = "~/Views/Shared/Error.cshtml"
            };
            */
        }
    }
}
