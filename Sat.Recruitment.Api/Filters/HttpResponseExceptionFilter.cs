using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sat.Recruitment.Infraestructure.Commons;
using System.Linq;

namespace Sat.Recruitment.Api.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.AllErrors().ToList();
                var result = OperationResponseFactory.CreateResponse("PARAMO_ERROR", "Validation is not correct", errors);
                context.Result = new BadRequestObjectResult(result);
            }
        }

    }
}
