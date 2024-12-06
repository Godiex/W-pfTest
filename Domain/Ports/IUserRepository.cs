using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface IUserRepository
    {
        Task<List<User>> GetLast10UsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
    }
}
