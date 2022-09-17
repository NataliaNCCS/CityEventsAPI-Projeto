using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityEventsAPI.Core.Models;

namespace CityEventsAPI.Core.Interfaces
{
    public interface IEventReservationRepository
    {
        public Task<List<EventReservation>> GetAllReservationsDBAsync();
        public Task<EventReservation> GetReservationByIdDBAsync(long IdReservation);
        public Task<object> GetReservationByPersonNameAndTitleAsync(string personName, string title);
        public Task<bool> InsertNewReservationDBAsync(EventReservation reservation);
        public Task<bool> UpdateReservationDBAsync(long IdReservation, long Quantity);
        public Task<List<EventReservation>> DeleteEventReservationDBAsync(long Id);

    }
}
