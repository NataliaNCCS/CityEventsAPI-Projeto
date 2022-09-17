using CityEventsAPI.Core.Models;
using CityEventsAPI.Core.Interfaces;

namespace CityEventsAPI.Core.Services
{
    public class CityEventService : ICityEventService
    {
        public ICityEventRepository _cityEventRepository;

        public CityEventService(ICityEventRepository cityEventRepository)
        {
            _cityEventRepository = cityEventRepository;
        }

        public async Task<List<CityEvent>> GetAllCityEventsAsync()
        {
            return await _cityEventRepository.GetAllEventsDBAsync();
        }


        public async Task<List<CityEvent>> GetCityEventsByNameAsync(string title)
        {
            return await _cityEventRepository.GetEventsByNameDBAsync(title);
        }


        public async Task<List<CityEvent>> GetCityEventsByLocalAndDateAsync(string local, DateTime dateHourEvent)
        {
            return await _cityEventRepository.GetEventsByLocalAndDateDBAsync(local, dateHourEvent);
        }


        public async Task<List<CityEvent>> GetCityEventsByPriceAndDateAsync(decimal min, decimal max, DateTime dateHourEvent)
        {
            return await _cityEventRepository.GetEventsByPriceAndDateDBAsync(min, max, dateHourEvent);
        }


        public async Task<CityEvent> GetCityEventsByIdAsync(long? id)
        {
            return await _cityEventRepository.GetEventByIdDBAsync(id);
        }


        public async Task<bool> InsertNewEventDBAsync(CityEvent cityEvent)
        {
            return await _cityEventRepository.InsertNewEventDBAsync(cityEvent);
        }


        public async Task<bool> UpdateCityEventAsync(long id, CityEvent cityEvent)
        {
            return await _cityEventRepository.UpdateEventDBAsync(id, cityEvent);
        }


        public async Task<bool> DeleteCityEventAsync(long id)
        {
            return await _cityEventRepository.DeleteEventDBAsync(id);
        }

        public async Task<bool> AlterStatusAsync(long Id)
        {
            return await _cityEventRepository.AlterStatusDBAsync(Id);
        }
    }
}
