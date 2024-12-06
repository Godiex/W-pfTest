using Dapper;
using Domain.Entities;
using Domain.Ports;
using Infrastructure.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Adapters
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IDbConnectionFactory _connectionFactory;

        public UserRepository(AppDbContext context, IDbConnectionFactory connectionFactory)
        {
            _context = context;
            _connectionFactory = connectionFactory;
        }

        public async Task<List<User>> GetLast10UsersAsync()
        {
            string query = "SELECT TOP 10 * FROM Users ORDER BY CreatedDate DESC";
            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                IEnumerable<User> result = await db.QueryAsync<User>(query);
                return result.ToList();
            }
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
