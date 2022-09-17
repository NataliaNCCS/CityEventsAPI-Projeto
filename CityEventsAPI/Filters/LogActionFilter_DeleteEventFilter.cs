using CityEventsAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CityEventsAPI.Filters
{
    public class LogActionFilter_DeleteEventFilter : ActionFilterAttribute
    {
        readonly IEventReservationService _eventReservationService;
        readonly ICityEventService _cityEventService;

        public LogActionFilter_DeleteEventFilter(IEventReservationService eventReservationService, ICityEventService cityEventService)
        {
            _eventReservationService = eventReservationService;
            _cityEventService = cityEventService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            long idEvent = (long)context.ActionArguments["idEvent"];

            if (_eventReservationService.DeleteEventReservationAsync(idEvent).Result.Count > 0)
            {
                _cityEventService.AlterStatusAsync(idEvent);
                var result = new ObjectResult(new { erro = "O evento está desativado pois possui reservas vigentes" })
                {
                    StatusCode = 204
                };
                context.Result = result;
            }
            else
            {
                return;
            }

        }


    }
}
