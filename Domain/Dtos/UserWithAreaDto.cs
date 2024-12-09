using System;

namespace Domain.Dtos
{
    public class UserWithAreaDto
    {
        public string Identification { get; set; } 
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Index { get; set; }
        
        public string AreaName { get; set; }
    }
}