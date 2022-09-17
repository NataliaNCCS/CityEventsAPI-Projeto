using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CityEventsAPI.Core.Models
{
    public class EventReservation
    {
        public long IdReservation { get; set; }

        [Required(ErrorMessage = "ID do evento é obrigatório")]
        public long IdEvent { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string PersonName { get; set; }

        [Required(ErrorMessage = "Quantidade de reservas é obrigatório")]
        public long? Quantity { get; set; }

    }
}
