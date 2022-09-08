using CityEventsAPI.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using CityEventsAPI.Core.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CityEventsAPI.Infra.Data
{
    public class CityEventRepository : ICityEventRepository
    {
        private readonly IConfiguration _configuration;

        public CityEventRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool DeleteEventDB(long idEvent)
        {
            var query = "DELETE FROM CityEvent WHERE IdEvent = @IdEvent";

            var parameters = new DynamicParameters();
            parameters.Add("IdEvent", idEvent);

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using var conn = new SqlConnection(connectionString);

            return conn.Execute(query, parameters) > 0;
        }

        public List<CityEvent> GetAllEventsDB()
        {
            var query = "SELECT * FROM CityEvent";

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using var conn = new SqlConnection(connectionString);

            return conn.Query<CityEvent>(query).ToList();
        }

        public CityEvent GetEventByIdDB(long idEvent)
        {
            var query = @"SELECT * FROM CityEvent WHERE IdEvent = @IdEvent";

            var parameters = new DynamicParameters();
            parameters.Add("IdEvent", idEvent);

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using var conn = new SqlConnection(connectionString);

            if(conn.Query<CityEvent>(query, parameters).ToList().Count() == 1)
            {
                return conn.QueryFirst<CityEvent>(query, parameters);
            }
            else
            {
                return null;
            }

        }

        public bool InsertNewEventDB(CityEvent cityEvent)
        {
            var query = "INSERT INTO CityEvent VALUES(@Title, @Description, @DateHourEvent, @Local, @Address, @Price)";

            var parameters = new DynamicParameters(new
            {
                cityEvent.Title,
                cityEvent.Description,
                cityEvent.DateHourEvent,
                cityEvent.Local,
                cityEvent.Address,
                cityEvent.Price
            });

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using var conn = new SqlConnection(connectionString);

            return (conn.Execute(query, parameters) > 0);


        }

        public bool UpdateEventDB(long idEvent, CityEvent cityEvent)
        {
            var query = @"UPDATE CityEvent
SET Title = @Title, Description = @Description, DateHourEvent = @DateHourEvent, Local = @Local, Address = @Address, Price = @Price
WHERE IdEvent = @IdEvent";

            var parameters = new DynamicParameters(new
            {
                cityEvent.IdEvent,
                cityEvent.Title,
                cityEvent.Description,
                cityEvent.DateHourEvent,
                cityEvent.Local,
                cityEvent.Address,
                cityEvent.Price
            });

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using var conn = new SqlConnection(connectionString);

            return conn.Execute(query, parameters) == 1;
        }
    }
}
