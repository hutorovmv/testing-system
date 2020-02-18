using System;

namespace TestingSystem.BLL.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }

        public byte[] ProfilePhoto { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string AboutMe { get; set; }
    }
}
