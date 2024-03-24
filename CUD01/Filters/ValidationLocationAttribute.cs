
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.RegularExpressions;
using University.Core.Models;

namespace CUD01.Filters
{
    public class ValidationLocationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            Department Department = context.ActionArguments["Department"] as Department;
            var regex = new Regex("^(cairo|giza|alex)");
            if (Department is null || !regex.IsMatch(Department.Location))
            {
                context.ModelState.AddModelError("Location", "Location is Not Valid");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }

            // base.OnActionExecuting(context);
        }
    }
}
