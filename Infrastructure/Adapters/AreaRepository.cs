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
            string query = "SELECT * FROM Areas ORDER BY Identification";
            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                IEnumerable<Area> result = await db.QueryAsync<Area>(query);
                return result.ToList();
            }
        }

        public async Task AssignUserToAreaAsync(string userIdentification, int areaId)
        {
            string query = @"
            INSERT INTO UserAreas (UserId, AreaId) 
            VALUES (@UserIdentification, @AreaId)";
    
            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                await db.ExecuteAsync(query, new { UserIdentification = userIdentification, AreaId = areaId });
            }
        }

        public async Task UpdateUserAreaAsync(string userIdentification, int areaId)
        {
            string query = @"
            UPDATE UserAreas 
            SET AreaId = @AreaId
            WHERE UserId = @UserIdentification";
    
            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                await db.ExecuteAsync(query, new { UserIdentification = userIdentification, AreaId = areaId });
            }
        }

        public async Task<bool> IsUserAssignedToAreaAsync(string userIdentification)
        {
            string query = @"
            SELECT COUNT(1) 
            FROM UserAreas 
            WHERE UserId = @UserIdentification";
    
            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                int count = await db.ExecuteScalarAsync<int>(query, new { UserIdentification = userIdentification });
                return count > 0;
            }
        }

    }
}
