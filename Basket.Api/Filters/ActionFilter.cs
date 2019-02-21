using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Basket.Api.Filters
{
    public class ActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var erros = from modelState in context.ModelState where modelState.Value.Errors.Any() select modelState.Value.Errors;

            var errorMessages = erros.SelectMany(item => item)
                        .Aggregate("", (current, message) => current + message + " ");

            if (!context.ModelState.IsValid)
            {
                context.Result = new ContentResult()
                {
                    Content = "Model is not valid " + errorMessages,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                };
            }

            base.OnActionExecuting(context);
        }
    }
}
