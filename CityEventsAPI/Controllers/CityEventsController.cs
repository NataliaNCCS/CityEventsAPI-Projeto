using CityEventsAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CityEventsAPI.Core.Models;


namespace CityEventsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]

    public class CityEventsController : ControllerBase
    {
        public ICityEventService _cityEventService;

        public CityEventsController(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }


        [HttpGet("/eventos/consultar")]
        public ActionResult<List<CityEvent>> GetEvents()
        {
            var eventos = _cityEventService.GetAllEvents();
            return Ok(eventos);
        }


        [HttpGet("/eventos/{idEvent}/consultar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetEventById(long idEvent)
        {
            var idConsultado = _cityEventService.GetCityEventsById(idEvent);

            if (idConsultado == null)
            {
                return BadRequest();
            }

            return Ok(idConsultado);
        }


        [HttpPost("/eventos/inserir")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult InsertEvent(CityEvent cityEvent)
        {
            bool eventoInserido = _cityEventService.InsertCityEvent(cityEvent);

            if (!eventoInserido)
            {
                return BadRequest();
            }

            return Created(nameof(InsertEvent), cityEvent);

        }

        [HttpPut("/eventos/{idEvent}/atualizar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult ÚpdateEvent(long idEvent, CityEvent cityEvent)
        {
            bool eventoAtualizado = _cityEventService.UpdateCityEvent(idEvent, cityEvent);

            if (!eventoAtualizado)
            {
                return BadRequest();
            }

            return NoContent();

        }


        [HttpDelete("/eventos/{idEvent}/deletar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteEvent(long idEvent)
        {
            var eventoDeletado = _cityEventService.DeleteCityEvent(idEvent);

            if (!eventoDeletado)
            {
                return BadRequest();
            }

            return NoContent();

        }
    }
}
