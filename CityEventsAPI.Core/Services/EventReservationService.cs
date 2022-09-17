using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityEventsAPI.Core.Interfaces;
using CityEventsAPI.Core.Models;

namespace CityEventsAPI.Core.Services
{
    public class EventReservationService : IEventReservationService
    {
        public IEventReservationRepository _eventReservationRepository;
        public EventReservationService(IEventReservationRepository eventReservationRepository)
        {
            _eventReservationRepository = eventReservationRepository;
        }


        public async Task<List<EventReservation>> GetAllEventsReservationsAsync()
        {
            return await _eventReservationRepository.GetAllReservationsDBAsync();
        }

        public async Task<EventReservation> GetEventReservationByIdAsync(long IdReservation)
        {
            return await _eventReservationRepository.GetReservationByIdDBAsync(IdReservation);
        }


        public async Task<object> GetEventReservationByPersonNameAndTitleAsync(string personName, string title)
        {
            return await _eventReservationRepository.GetReservationByPersonNameAndTitleAsync(personName, title);
        }


        public async Task<bool> InsertNewEventReservationAsync(EventReservation reservation)
        {
            return await _eventReservationRepository.InsertNewReservationDBAsync(reservation);
        }


        public async Task<bool> UpdateEventReservationAsync(long IdReservation, long Quantity)
        {
            return await _eventReservationRepository.UpdateReservationDBAsync(IdReservation, Quantity);
        }

        public async Task<List<EventReservation>> DeleteEventReservationAsync(long Id)
        {
            return await _eventReservationRepository.DeleteEventReservationDBAsync(Id);
        }


    }
}
