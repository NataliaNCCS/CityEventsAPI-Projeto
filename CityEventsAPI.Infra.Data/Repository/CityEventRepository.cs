using CityEventsAPI.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using CityEventsAPI.Core.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CityEventsAPI.Infra.Data.Repository
{
    public class CityEventRepository : ICityEventRepository
    {
        private readonly IConfiguration _configuration;

        public CityEventRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<List<CityEvent>> GetAllEventsDBAsync() // ok
        {
            var query = "SELECT * FROM CityEvent WHERE Status = 1";

            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                using var conn = new SqlConnection(connectionString);

                return (await conn.QueryAsync<CityEvent>(query)).ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar se comunicar com banco de dados. \n\nMessage: {ex.Message} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }

        }


        public async Task<List<CityEvent>> GetEventsByNameDBAsync(string Title) // OK
        {
            var query = $"SELECT * FROM CityEvent WHERE Title LIKE ('%' +  @Title + '%')";
            var parameters = new DynamicParameters();
            parameters.Add("Title", Title);

            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                using var conn = new SqlConnection(connectionString);

                return (await conn.QueryAsync<CityEvent>(query, parameters)).ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar se comunicar com banco de dados. \n\nMessage: {ex.Message} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<List<CityEvent>> GetEventsByLocalAndDateDBAsync(string local, DateTime dateHourEvent)
        {
            var query = $"SELECT * FROM CityEvent WHERE Local LIKE ('%' + @local + '%') AND CONVERT(DATE, DateHourEvent)= @dateHourEvent";
            var parameters = new DynamicParameters();
            parameters.Add("Local", local);
            parameters.Add("DateHourEvent", dateHourEvent);


            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                using var conn = new SqlConnection(connectionString);

                return (await conn.QueryAsync<CityEvent>(query, parameters)).ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar se comunicar com banco de dados. \n\nMessage: {ex.Message} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }
        }


        public async Task<List<CityEvent>> GetEventsByPriceAndDateDBAsync(decimal min, decimal max, DateTime dateHourEvent)
        {
            var query = "SELECT * FROM CityEvent WHERE CONVERT(DATE, DateHourEvent) = @dateHourEvent AND price BETWEEN @min AND @max";

            var parameters = new DynamicParameters(new
            {
                dateHourEvent,
                min,
                max
            });

            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                using var conn = new SqlConnection(connectionString);

                return (await conn.QueryAsync<CityEvent>(query, parameters)).ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar se comunicar com banco de dados. \n\nMessage: {ex.Message} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }



        }

        public async Task<CityEvent> GetEventByIdDBAsync(long? IdEvent) 
        {
            var query = @"SELECT * FROM CityEvent WHERE IdEvent = @IdEvent";

            var parameters = new DynamicParameters();
            parameters.Add("IdEvent", IdEvent);


            try
            {

                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                using var conn = new SqlConnection(connectionString);

                return await conn.QueryFirstOrDefaultAsync<CityEvent>(query, parameters);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar se comunicar com banco de dados. \n\nMessage: {ex.Message} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }


        }


        public async Task<bool> InsertNewEventDBAsync(CityEvent cityEvent)
        {
            var query = "INSERT INTO CityEvent VALUES(@Title, @Description, @DateHourEvent, @Local, @Address, @Price, @Status)";

            var parameters = new DynamicParameters(new
            {
                cityEvent.Title,
                cityEvent.Description,
                cityEvent.DateHourEvent,
                cityEvent.Local,
                cityEvent.Address,
                cityEvent.Price,
                cityEvent.Status
            });

            try
            {

                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                using var conn = new SqlConnection(connectionString);

                return await conn.ExecuteAsync(query, parameters) == 1;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar se comunicar com banco de dados. \n\nMessage: {ex.Message} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }


        }


        public async Task<bool> UpdateEventDBAsync(long idEvent, CityEvent cityEvent)
        {
            var query = @"UPDATE CityEvent
            SET Title = @Title, Description = @Description, DateHourEvent = @DateHourEvent, 
            Local = @Local, Address = @Address, Price = @Price, Status = @Status
            WHERE IdEvent = @IdEvent";

            var parameters = new DynamicParameters(cityEvent);



            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                using var conn = new SqlConnection(connectionString);

                return await conn.ExecuteAsync(query, parameters) == 1;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar se comunicar com banco de dados. \n\nMessage: {ex.Message} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }
        }


        public async Task<bool> DeleteEventDBAsync(long idEvent)
        {
            var query = "DELETE FROM CityEvent WHERE IdEvent = @IdEvent";

            var parameters = new DynamicParameters();
            parameters.Add("IdEvent", idEvent);


            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                using var conn = new SqlConnection(connectionString);

                return await conn.ExecuteAsync(query, parameters) == 1;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar se comunicar com banco de dados. \n\nMessage: {ex.Message} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }
        }


        public async Task<bool> AlterStatusDBAsync(long IdEvent)
        {
            var query = @"UPDATE CityEvent SET Status = 0
                          WHERE cityEvent.IdEvent = @IdEvent";
            DynamicParameters parameters = new(new { IdEvent });

            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                using var conn = new SqlConnection(connectionString);
                return await conn.ExecuteAsync(query, parameters) == 1;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar se comunicar com banco de dados. \n\nMessage: {ex.Message} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }
        }


        
    }
}
