using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityEventsAPI.Core.Interfaces;
using CityEventsAPI.Core.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CityEventsAPI.Infra.Data.Repository
{
    public class EventReservationRepository : IEventReservationRepository
    {
        private readonly IConfiguration _configuration;

        public EventReservationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<EventReservation>> GetAllReservationsDBAsync()
        {
            var query = "SELECT * FROM EventReservation";


            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                using var conn = new SqlConnection(connectionString);

                return (await conn.QueryAsync<EventReservation>(query)).ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar se comunicar com banco de dados. \n\nMessage: {ex.Message} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }


        }

        public async Task<EventReservation> GetReservationByIdDBAsync(long IdReservation)
        {
            var query = "SELECT * FROM EventReservation WHERE IdReservation = @IdReservation";

            var parameters = new DynamicParameters(new
            {
                IdReservation
            });


            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                using var conn = new SqlConnection(connectionString);

                return await conn.QueryFirstOrDefaultAsync<EventReservation>(query, parameters);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar se comunicar com banco de dados. \n\nMessage: {ex.Message} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }

        }

        public async Task<object> GetReservationByPersonNameAndTitleAsync(string personName, string title)
        {
            var query = @"SELECT * FROM EventReservation AS res 
INNER JOIN CityEvent AS city
ON res.PersonName = @personName AND city.Title LIKE  ('%' +  @Title + '%')";

            var parameters = new DynamicParameters(new
            {
                personName,
                title
            });


            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                using var conn = new SqlConnection(connectionString);

                return (await conn.QueryAsync<EventReservation>(query, parameters)).ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar se comunicar com banco de dados. \n\nMessage: {ex.Message} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<bool> InsertNewReservationDBAsync(EventReservation reservation)
        {
            var query = "INSERT INTO EventReservation VALUES(@IdEvent, @PersonName, @Quantity)";

            var query2 = "SELECT * FROM CityEvent WHERE IdEvent = @IdEvent";

            var parameters = new DynamicParameters(new
            {
                reservation.IdEvent,
                reservation.PersonName,
                reservation.Quantity,
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



        public async Task<bool> DeleteReservationDBAsync(long IdReservation)
        {
            var query = "DELETE FROM EventReservation WHERE IdReservation = @IdReservation";

            var parameters = new DynamicParameters();
            parameters.Add("IdReservation", IdReservation);

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



        public async Task<bool> UpdateReservationDBAsync(long IdReservation, long Quantity)
        {
            var query = @"UPDATE EventReservation
SET Quantity = @Quantity
WHERE IdReservation = @IdReservation";

            var parameters = new DynamicParameters(new
            {
                Quantity,
                IdReservation
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


        public async Task<List<EventReservation>> DeleteEventReservationDBAsync(long IdEvent)
        {
            var query = "SELECT * FROM EventReservation WHERE IdEvent = @IdEvent";
            DynamicParameters parameters = new(new { IdEvent });

            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                using var conn = new SqlConnection(connectionString);

                return (await conn.QueryAsync<EventReservation>(query, parameters)).ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar se comunicar com banco de dados. \n\nMessage: {ex.Message} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }
        }
    }
}
