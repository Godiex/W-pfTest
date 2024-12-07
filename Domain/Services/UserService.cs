using Domain.Entities;
using Domain.Ports;
using System;
using System.Threading.Tasks;

namespace Domain.Services
{
    [DomainService]
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Create(User user)
        {
            bool existsByIdentification = await _userRepository.ExistsByIdentificationAsync(user.Identification);
            if (existsByIdentification)
            {
                throw new Exception("Ya existe un usuario con esta identificación.");
            }
            await ValidateContactDataAsync(user.Email, user.Phone);

            await _userRepository.AddAsync(user);
        }


        private async Task ValidateContactDataAsync(string email, string phone)
        {
            bool existsByContactData = await _userRepository.ExistsByContactDataAsync(email, phone);
            if (existsByContactData)
            {
                throw new Exception("Ya existe un usuario con estos datos de contacto.");
            }
        }

        public async Task UpdateContactDataAsync(string identification, string email, string phone)
        {
            bool existsByIdentification = await _userRepository.ExistsByIdentificationAsync(identification);
            if (!existsByIdentification)
            {
                throw new Exception($"Error no existe el usuario con identificacion {identification}");
            }

            await ValidateContactDataAsync(email, phone);

            User userToUpdate = await _userRepository.GetByIdentificationAsync(identification);

            userToUpdate.UpdateContactData(email, phone);

            await _userRepository.UpdateAsync(userToUpdate);
        }
    }
}
