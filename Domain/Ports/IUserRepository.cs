using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface IUserRepository
    {
        Task<List<User>> GetLast10UsersAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task<User> GetByIdentificationAsync(string identification);
        Task<bool> ExistsByIdentificationAsync(string identification);
        Task<bool> ExistsByContactDataAsync(string email, string phone, string userId = null);
    }
}
