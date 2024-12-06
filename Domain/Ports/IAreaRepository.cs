using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface IAreaRepository
    {
        Task<List<Area>> GetAllAreasAsync();
        Task AssignUserToAreaAsync(int userId, int areaId);
    }
}
