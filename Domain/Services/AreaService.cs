using Domain.Entities;
using Domain.Ports;
using System;
using System.Threading.Tasks;

namespace Domain.Services
{
    [DomainService]
    public class AreaService
    {
        private readonly IAreaRepository _areaRepository;
        private readonly IUserRepository _userRepository;

        public AreaService(IAreaRepository areaRepository, IUserRepository userRepository)
        {
            _areaRepository = areaRepository;
            _userRepository = userRepository;
        }

        public async Task AssignUserToAreaAsync(string userIdentification, int areaId)
        {
            bool existsByIdentification = await _userRepository.ExistsByIdentificationAsync(userIdentification);
            if (!existsByIdentification)
            {
                throw new Exception($"Error no existe el usuario con identificacion {userIdentification}");
            }

        }
    }
}
