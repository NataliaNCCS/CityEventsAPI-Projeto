using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityEventsAPI.Core.Models;

namespace CityEventsAPI.Core.Interfaces
{
    public interface IEventReservationService
    {
        public Task<List<EventReservation>> GetAllEventsReservationsAsync();
        public Task<EventReservation> GetEventReservationByIdAsync(long IdReservation);
        public Task<object> GetEventReservationByPersonNameAndTitleAsync(string personName, string title);
        public Task<bool> InsertNewEventReservationAsync(EventReservation reservation);
        public Task<bool> UpdateEventReservationAsync(long IdReservation, long Quantity);
        public Task<List<EventReservation>> DeleteEventReservationAsync(long Id);

    }
}
