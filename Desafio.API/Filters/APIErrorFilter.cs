using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Desafio.API.Filters
{
    public class APIErrorFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new ObjectResult("There was a problem while handling the request")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
