using CityEventsAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CityEventsAPI.Filters
{
    public class LogActionFilter_CityEventExists : ActionFilterAttribute
    {
        ICityEventService _cityEventService;

        public LogActionFilter_CityEventExists(ICityEventService clienteService)
        {
            _cityEventService = clienteService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            long idEvent = (long)context.ActionArguments["IdEvent"];

            if (_cityEventService.GetCityEventsByIdAsync(idEvent) == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }
    }
}
