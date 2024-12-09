using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Ports;

namespace Application.UseCase.GetLast10UsersHandler
{
    [Handler]
    public class GetLast10UsersHandler
    {
        private readonly IUserRepository _userRepository;

        public GetLast10UsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>> Handle()
        {
            List<User> users = await _userRepository.GetLast10UsersAsync();
            return users.ConvertAll(x => new UserDto(x.Identification, x.FullName, x.Email, x.Phone, x.CreatedDate));
        }
    }
}
