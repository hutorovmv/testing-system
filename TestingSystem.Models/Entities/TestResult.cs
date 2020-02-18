using System;
using System.Collections.Generic;

namespace TestingSystem.Models.Entities
{
    public class TestResult
    {
        public Guid Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public int CorrectAnswers { get; set; }

        public string UserId { get; set; }
        public UserProfile User { get; set; }

        public Guid TestId { get; set; }
        public Test Test { get; set; }

        public ICollection<TestAnswer> TestAnswers { get; set; }

        public TestResult()
        {
            TestAnswers = new List<TestAnswer>();
        }
    }
}
