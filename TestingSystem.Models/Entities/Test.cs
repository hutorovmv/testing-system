using System;
using System.Collections.Generic;

namespace TestingSystem.Models.Entities
{
    public class Test
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? TimeRequired { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }

        public string AuthorId { get; set; }
        public UserProfile Author { get; set; }

        public ICollection<Question> Questions { get; set; }
        public ICollection<TestResult> TestResults { get; set; }
    }
}
