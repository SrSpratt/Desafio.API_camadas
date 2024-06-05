using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Desafio.API.Filters
{
    public class APIErrorFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new ObjectResult(context.Exception.Message) // isso ou uma mensagem padrão
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
