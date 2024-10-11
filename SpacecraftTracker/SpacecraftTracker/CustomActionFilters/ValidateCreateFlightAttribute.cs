using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SpacecraftTracker.Persistence;
using SpacecraftTracker.WebAPI.DTO;
using SpacecraftTracker.Domain.Entities;

namespace SpacecraftTracker.WebAPI.CustomActionFilters
{
    public class ValidateCreateFlightAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var flight = context.ActionArguments["addFlight"] as FlightDTO;

            var db = (SpacecraftTrackerDbContext)context.HttpContext.RequestServices.GetService(typeof(SpacecraftTrackerDbContext));

            if (db.Flights.Any(a => a.FlightNumber.ToLower() == flight.FlightNumber.ToLower()))
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                context.Result = new ConflictResult();
            }

            if (!db.Spacecrafts.Any(x => x.Id == flight.SpacecraftId))
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                context.Result = new ConflictResult();
            }

            base.OnActionExecuting(context);
        }
    }
}
