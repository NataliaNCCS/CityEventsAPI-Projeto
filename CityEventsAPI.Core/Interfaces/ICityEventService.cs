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
        List<CityEvent> GetAllEvents();
        CityEvent GetCityEventsById(long id);
        bool InsertCityEvent(CityEvent cityEvent);
        bool UpdateCityEvent(long id, CityEvent cityEvent);
        bool DeleteCityEvent(long id);
    }
}
