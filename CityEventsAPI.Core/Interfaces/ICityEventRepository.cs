using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityEventsAPI.Core.Models;

namespace CityEventsAPI.Core.Interfaces
{
    public interface ICityEventRepository
    {
        public Task<List<CityEvent>> GetAllEventsDBAsync();
        public Task<List<CityEvent>> GetEventsByNameDBAsync(string Title);
        public Task<CityEvent> GetEventByIdDBAsync(long? idEvent);
        public Task<List<CityEvent>> GetEventsByLocalAndDateDBAsync(string local, DateTime dateHourEvent);
        public Task<List<CityEvent>> GetEventsByPriceAndDateDBAsync(decimal min, decimal max, DateTime dateHourEvent);

        public Task<bool> InsertNewEventDBAsync(CityEvent cityEvent);
        public Task<bool> DeleteEventDBAsync(long idEvent);
        public Task<bool> UpdateEventDBAsync(long idEvent, CityEvent cityEvent);

        public Task<bool> AlterStatusDBAsync(long Id);

    }
}
