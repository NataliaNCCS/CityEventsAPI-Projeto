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
    [Authorize(Roles = "admin, cliente")]

    public class CityEventsController : ControllerBase
    {
        public ICityEventService _cityEventService;

        public CityEventsController(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        [HttpGet("/eventos/consultarTodosOsEventos")]
        [Authorize(Roles = "admin, cliente")]
        public async Task<ActionResult<List<CityEvent>>> GetEvents()
        {
            var eventos = _cityEventService.GetAllCityEventsAsync();
            return Ok(await eventos);
        }

        [HttpGet("/eventos/consultarEventosPorNome")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "admin, cliente")]
        public async Task<IActionResult> GetEventsByName(string nome)
        {
            return Ok(await _cityEventService.GetCityEventsByNameAsync(nome));
        }


        [HttpGet("/eventos/consultarEventosPorLocalEData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "admin, cliente")]
        public async Task<IActionResult> GetEventsByLocalAndDate(string local, DateTime date)
        {
            return Ok(await _cityEventService.GetCityEventsByLocalAndDateAsync(local, date));
        }

        [HttpGet("/eventos/consultarEventosPorPrecoEData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "admin, cliente")]
        public async Task<IActionResult> GetEventsByPriceAndDate(decimal min, decimal max, DateTime date)
        {
            return Ok(await _cityEventService.GetCityEventsByPriceAndDateAsync(min, max, date));
        }



        [HttpGet("/eventos/{idEvent}/consultarEventoPorId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ServiceFilter(typeof(LogActionFilter_CityEventExists))]
        [Authorize(Roles = "admin, cliente")]
        public async Task<IActionResult> GetEventById(long idEvent)
        {
            return Ok(await _cityEventService.GetCityEventsByIdAsync(idEvent));
        }


        [HttpPost("/eventos/inserirEvento")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> InsertEvent(CityEvent cityEvent)
        {
        
            if (! await _cityEventService.InsertNewEventDBAsync(cityEvent))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Created(nameof(InsertEvent), cityEvent);

        }

        [HttpPut("/eventos/{idEvent}/atualizarEvento")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ServiceFilter(typeof(LogActionFilter_CityEventExists))]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateEvent(long idEvent, CityEvent cityEvent)
        {

            if (! await _cityEventService.UpdateCityEventAsync(idEvent, cityEvent))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return NoContent();

        }


        [HttpDelete("/eventos/{idEvent}/deletarEvento")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ServiceFilter(typeof(LogActionFilter_CityEventExists))]
        [ServiceFilter(typeof(LogActionFilter_DeleteEventFilter))]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteEvent(long idEvent)
        {

            if (! await _cityEventService.DeleteCityEventAsync(idEvent))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return NoContent();

        }
    }
}
