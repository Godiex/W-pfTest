using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Domain.Entities;
using Domain.Ports;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
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

        public async Task AssignUserToAreaAsync(int userId, int areaId)
        {
            string query = "INSERT INTO UserAreas (UserId, AreaId) VALUES (@UserId, @AreaId)";
            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                await db.ExecuteAsync(query, new { UserId = userId, AreaId = areaId });
            }
        }
    }
}
