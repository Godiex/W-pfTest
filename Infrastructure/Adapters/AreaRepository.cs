using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Domain.Entities;
using Domain.Ports;
using Infrastructure.Data;
using Infrastructure.Extensions;

namespace Infrastructure.Adapters
{
    [Repository]
    public class AreaRepository : IAreaRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public AreaRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<Area>> GetAllAreasAsync()
        {
            string query = "SELECT * FROM Areas";
            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                IEnumerable<Area> result = await db.QueryAsync<Area>(query);
                return result.ToList();
            }
        }

        public async Task AssignUserToAreaAsync(string userIdentification, int areaId)
        {
            string query = "INSERT INTO UserAreas ('UserId', AreaId) VALUES (@UserIdentification, @AreaId)";
            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                await db.ExecuteAsync(query, new { UserIdentification = userIdentification, AreaId = areaId });
            }
        }

        public async Task<bool> ExistsByIdentificationAsync(int id)
        {
            string query = @"
            SELECT COUNT(1) 
            FROM Users 
            WHERE Id = @Identification";

            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                int count = await db.ExecuteScalarAsync<int>(query, new
                {
                    Id = id
                });
                return count > 0;
            }
        }
    }
}
