using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SpacecraftTracker.Persistence;
using SpacecraftTracker.WebAPI.DTO;

namespace SpacecraftTracker.WebAPI.CustomActionFilters
{
    public class ValidateEmailAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var registerRequest = context.ActionArguments["registerRequest"] as AuthDTO;

            var db = (SpacecraftTrackerDbContext)context.HttpContext.RequestServices.GetService(typeof(SpacecraftTrackerDbContext));
            if (db.Users.Any(a => a.UserName == registerRequest.Username))
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                context.Result = new ConflictResult();
            }
            base.OnActionExecuting(context);
        }
    }
}
