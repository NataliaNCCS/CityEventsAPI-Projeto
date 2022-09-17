using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityEventsAPI.Core.Models;

namespace CityEventsAPI.Core.Interfaces
{
    public interface ICityEventService
    {
        public Task<List<CityEvent>> GetAllCityEventsAsync();
        public Task<List<CityEvent>> GetCityEventsByNameAsync(string title);
        public Task<List<CityEvent>> GetCityEventsByLocalAndDateAsync(string local, DateTime dateHourEvent);
        public Task<List<CityEvent>> GetCityEventsByPriceAndDateAsync(decimal min, decimal max, DateTime dateHourEvent);
        public Task<CityEvent> GetCityEventsByIdAsync(long? id);
        public Task<bool> InsertNewEventDBAsync(CityEvent cityEvent);
        public Task<bool> UpdateCityEventAsync(long id, CityEvent cityEvent);
        public Task<bool> DeleteCityEventAsync(long id);
        public Task<bool> AlterStatusAsync(long Id);

    }
}
