using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;

namespace Desafio.Consumer.Services.Filters
{
    public class SetTempModelStateAttribute : ActionFilterAttribute, IActionFilter
    { 
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = context.Controller as Controller;
            var modelState = controller?.ViewData.ModelState;
            if (modelState != null)
            {
                var listError = modelState.Where(model => model.Value.Errors.Any()).ToDictionary(model => model.Key, model => model.Value.Errors.Select(key => key.ErrorMessage).FirstOrDefault(key => key != null));
                controller.TempData["ModelErrors"] = JsonSerializer.Serialize(listError, new JsonSerializerOptions(JsonSerializerDefaults.Web));
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }

    public class RestoreTempModelStateAttribute : ActionFilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as Controller;
            var tempData = controller?.TempData?.Keys;
            if (controller != null && tempData != null)
            {
                if (tempData.Contains("ModelErrors"))
                {
                    var modelStateString = controller.TempData["ModelErrors"].ToString();
                    var listError = JsonSerializer.Deserialize<Dictionary<string, string>>(modelStateString, new JsonSerializerOptions(JsonSerializerDefaults.Web));
                    var modelState = new ModelStateDictionary();
                    foreach (var item in listError)
                    {
                        modelState.AddModelError(item.Key, item.Value ?? "");
                    }

                    controller.ViewData.ModelState.Merge(modelState);
                }
            }
        }
    }
}
