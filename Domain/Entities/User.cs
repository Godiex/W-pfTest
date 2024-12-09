using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class User
    {
        public string Identification { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedDate { get; set; }

        public User()
        {

        }

        public User(string identification, string fullName, string email, string phone)
        {
            Identification = identification;
            FullName = fullName;
            Email = email;
            Phone = phone;
            CreatedDate = DateTime.Now;
        }

        public void UpdateContactData(string email, string phone)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("El correo electrónico no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentException("El número de teléfono no puede estar vacío.");
            }

            Email = email;
            Phone = phone;
        }
    }
}
