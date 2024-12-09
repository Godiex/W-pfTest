using System.Threading.Tasks;
using Domain.Entities;
using Domain.Services;

namespace Application.UseCase.CreateUser
{
    [Handler]
    public class CreateUserHandler
    {
        private readonly UserService _userService;

        public CreateUserHandler(UserService userService)
        {
            _userService = userService;
        }

        public async Task Handle(CreateUserCommand request)
        {
            User user = new User(request.Identification, request.FullName, request.Email, request.Phone);
            await _userService.Create(user);
        }
    }
}
