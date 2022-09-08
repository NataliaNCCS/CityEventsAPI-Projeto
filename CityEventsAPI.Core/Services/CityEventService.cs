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

        public bool DeleteCityEvent(long id)
        {
            return _cityEventRepository.DeleteEventDB(id);
        }

        public List<CityEvent> GetAllEvents()
        {
            return _cityEventRepository.GetAllEventsDB();
        }

        public CityEvent GetCityEventsById(long id)
        {
            return _cityEventRepository.GetEventByIdDB(id);
        }

        public bool InsertCityEvent(CityEvent cityEvent)
        {
            return _cityEventRepository.InsertNewEventDB(cityEvent);
        }

        public bool UpdateCityEvent(long id, CityEvent cityEvent)
        {
            return _cityEventRepository.UpdateEventDB(id, cityEvent);
        }
    }
}
