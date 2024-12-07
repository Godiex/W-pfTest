using MediatR;

namespace Application.CreateUser
{
    public class CreateUserCommand : IRequest<Unit>
    {
        public string Identification { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
