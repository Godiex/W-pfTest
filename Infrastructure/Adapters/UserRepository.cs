using Dapper;
using Domain.Entities;
using Domain.Ports;
using Infrastructure.Data;
using Infrastructure.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dtos;

namespace Infrastructure.Adapters
{
    [Repository]
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IDbConnectionFactory _connectionFactory;

        public UserRepository(AppDbContext context, IDbConnectionFactory connectionFactory)
        {
            _context = context;
            _connectionFactory = connectionFactory;
        }

        public async Task<List<UserWithAreaDto>> GetLast10UsersAsync()
        {
            string query = @"
            SELECT TOP 10 
                u.Identification,
                u.FullName,
                u.Email,
                u.Phone,
                u.CreatedDate,
                ISNULL(a.Name, 'No Asignado') AS AreaName
            FROM Users u
            LEFT JOIN UserAreas ua ON u.Identification = ua.UserId
            LEFT JOIN Areas a ON ua.AreaId = a.Identification
            ORDER BY u.CreatedDate DESC";

            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                IEnumerable<UserWithAreaDto> result = await db.QueryAsync<UserWithAreaDto>(query);
                return result.ToList();
            }
        }


        public async Task<User> GetByIdentificationAsync(string identification)
        {
            string query = @"
            SELECT Identification, FullName, Email, Phone, CreatedDate 
            FROM Users 
            WHERE Identification = @Identification";

            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                User user = await db.QueryFirstOrDefaultAsync<User>(query, new
                {
                    Identification = identification
                });
                return user;
            }
        }


        public async Task<bool> ExistsByIdentificationAsync(string identification)
        {
            string query = @"
            SELECT COUNT(1) 
            FROM Users 
            WHERE Identification = @Identification";

            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                int count = await db.ExecuteScalarAsync<int>(query, new
                {
                    Identification = identification
                });
                return count > 0;
            }
        }

        public async Task<bool> ExistsByContactDataAsync(string email, string phone, string userId = null)
        {
            string query = @"
            SELECT COUNT(1) 
            FROM Users 
            WHERE (Email = @Email OR Phone = @Phone)
                  AND (@UserId IS NULL OR Identification != @UserId)";

            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                int count = await db.ExecuteScalarAsync<int>(query, new
                {
                    Email = email,
                    Phone = phone,
                    UserId = userId
                });
                return count > 0;
            }
        }

        

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
