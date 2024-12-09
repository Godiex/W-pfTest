using System;

namespace Application.UseCase.GetLast10UsersHandler
{
    public class UserDto
    {
        public string Identification { get; set; } 
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Index { get; set; }

        public UserDto(string identification, string fullName, string email, string phone, DateTime createdDate)
        {
            Identification = identification;
            FullName = fullName;
            Email = email;
            Phone = phone;
            CreatedDate = createdDate;
        }
    }
}
