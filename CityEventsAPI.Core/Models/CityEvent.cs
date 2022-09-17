using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CityEventsAPI.Core.CustomAttributes;

namespace CityEventsAPI.Core.Models
{
    public class CityEvent
    {
        public long IdEvent { get; set; }

        [Required(ErrorMessage = "Título é obrigatório")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [CustomDateRange(ErrorMessage = "Data inválida")]
        public DateTime? DateHourEvent { get; set; }

        [Required(ErrorMessage = "Local é obrigatório")]
        public string Local { get; set; }

        [Required(ErrorMessage = "Endereço é obrigatório")]
        public string Address { get; set; }

        public decimal Price { get; set; }

        [Required(ErrorMessage = "Status é obrigatório")]
        public bool Status { get; set; } = true;

    }
}
