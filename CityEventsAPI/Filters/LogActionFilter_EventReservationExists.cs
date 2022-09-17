using CityEventsAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CityEventsAPI.Filters
{
    public class LogActionFilter_EventReservationExists : ActionFilterAttribute
    {
        IEventReservationService _eventReservationService;

        public LogActionFilter_EventReservationExists(IEventReservationService eventReservation)
        {
            _eventReservationService = eventReservation;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            long idReservation = (long)context.ActionArguments["IdReservation"];

            if (_eventReservationService.GetEventReservationByIdAsync(idReservation) == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }
    }
}
