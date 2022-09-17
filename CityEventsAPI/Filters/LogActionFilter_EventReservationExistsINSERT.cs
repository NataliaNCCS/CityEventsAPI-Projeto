using CityEventsAPI.Core.Interfaces;
using CityEventsAPI.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CityEventsAPI.Filters
{
    public class LogActionFilter_EventReservationExistsINSERT : ActionFilterAttribute
    {
        ICityEventService _cityEventService;

        public LogActionFilter_EventReservationExistsINSERT(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            EventReservation reservation = (EventReservation)context.ActionArguments["reservation"];

            var list = _cityEventService.GetAllCityEventsAsync().Result.ToList();
            var resultado = list.Any(x => x.IdEvent == reservation.IdEvent);
            if (!resultado)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }
    }
}
