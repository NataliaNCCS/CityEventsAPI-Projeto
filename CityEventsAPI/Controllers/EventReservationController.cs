using CityEventsAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CityEventsAPI.Core.Models;
using CityEventsAPI.Filters;
using Microsoft.AspNetCore.Authorization;

namespace CityEventsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class EventReservationController : ControllerBase
    {
        public IEventReservationService _eventReservationService;

        public EventReservationController(IEventReservationService eventReservationService)
        {
            _eventReservationService = eventReservationService;
        }


        [HttpGet("/reservas/todasAsReservas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "admin, cliente")]
        public async Task<ActionResult<List<EventReservation>>> GetReservationsAsync()
        {
            var reservas = await _eventReservationService.GetAllEventsReservationsAsync();
            return Ok(reservas);
        }


        [HttpGet("/reservas/{IdReservation}")]
        [ServiceFilter(typeof(LogActionFilter_EventReservationExists))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "admin, cliente")]
        public async Task<ActionResult> GetReservationByIdAsync(long IdReservation)
        {
            var idConsultado = await _eventReservationService.GetEventReservationByIdAsync(IdReservation);

            if (idConsultado == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok(idConsultado);
        }

        [HttpGet("/reservas/consultar/{personName}/{title}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "admin, cliente")]
        public async Task<ActionResult> GetReservationByPersonNameAndTitleAsync(string personName, string title)
        {
            var reservaConsultada = await _eventReservationService.GetEventReservationByPersonNameAndTitleAsync(personName, title);

            if (reservaConsultada == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok(reservaConsultada);
        }




        [HttpPost("/reservas/inserir")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(LogActionFilter_EventReservationExistsINSERT))]
        [Authorize(Roles = "admin, cliente")]
        public async Task<IActionResult> InsertReservation(EventReservation reservation)
        {
            bool reservaInserida = await _eventReservationService.InsertNewEventReservationAsync(reservation);

            if (!reservaInserida)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Created(nameof(InsertReservation), reservation);

        }


        [HttpPut("/reservas/atualizarReserva/{IdReservation}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(LogActionFilter_EventReservationExists))]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> UpdateReservation(long IdReservation, long quantity)
        {
            bool reservaAtualizada = await _eventReservationService.UpdateEventReservationAsync(IdReservation, quantity);

            if (!reservaAtualizada)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return NoContent();

        }


        [HttpDelete("/reservas/deletar/{IdReservation}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(LogActionFilter_EventReservationExists))]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> DeleteReservation(long idReservation)
        {
            var reservaDeletada = await _eventReservationService.DeleteEventReservationAsync(idReservation);

            if (!reservaDeletada)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return NoContent();

        }

    }
}
