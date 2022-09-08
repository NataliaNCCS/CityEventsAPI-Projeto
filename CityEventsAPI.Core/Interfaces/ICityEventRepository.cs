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
        List<CityEvent> GetAllEventsDB();
        public bool InsertNewEventDB(CityEvent cityEvent);
        public bool DeleteEventDB(long id);
        public bool UpdateEventDB(long id, CityEvent cityEvent);
        public CityEvent GetEventByIdDB(long id);

    }
}
