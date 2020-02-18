using System;
using System.Collections.Generic;

namespace TestingSystem.Models.Entities
{
    public class UserProfile
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public DateTime? BirthDate { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string AboutMe { get; set; }
        
        public byte[] ProfilePhoto { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<Test> Tests { get; set; }
        public ICollection<TestResult> TestResults { get; set; }
    }
}
