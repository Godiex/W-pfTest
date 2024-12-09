using System.Threading.Tasks;
using Domain.Services;

namespace Application.UseCase.UpdateContactData
{
    [Handler]
    public class UpdateContactDataHandler
    {
        private readonly UserService _userService;

        public UpdateContactDataHandler(UserService userService)
        {
            _userService = userService;
        }

        public async Task Handle(UpdateContactDataCommand request)
        {
            await _userService.UpdateContactDataAsync(request.Identification, request.Email, request.Phone);
        }
    }
}
