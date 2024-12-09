using MediatR;

namespace Application.UseCase.CreateUser
{
    public class CreateUserCommand
    {
        public string Identification { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public CreateUserCommand(string identification, string fullName, string email, string phone)
        {
            Identification = identification;
            FullName = fullName;
            Email = email;
            Phone = phone;
        }
    }
}
