using Domain.Entities;
using Domain.Ports;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User user = new User(request.Identification, request.FullName, request.Email, request.Phone);
            await _userRepository.AddAsync(user);
            return new Unit();
        }
    }
}
