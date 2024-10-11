using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SpacecraftTracker.Persistence;
using SpacecraftTracker.WebAPI.DTO;

namespace SpacecraftTracker.WebAPI.CustomActionFilters
{
    public class ValidateCallsignAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var spacecraft = context.ActionArguments["createSpacecraft"] as CreateSpacecraftDTO;

            var db = (SpacecraftTrackerDbContext)context.HttpContext.RequestServices.GetService(typeof(SpacecraftTrackerDbContext));
            if (db.Spacecrafts.Any(a => a.Callsign.ToLower() == spacecraft.Callsign.ToLower()))
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                context.Result = new ConflictResult();
            }

            base.OnActionExecuting(context);
        }
    }
}
