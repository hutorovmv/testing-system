using System;
using System.Collections.Generic;

namespace TestingSystem.Models.Entities
{
    public class TestAnswer
    {
        public Guid Id { get; set; }
        public bool IsCorrect { get; set; }

        public Guid TestResultId { get; set; }
        public TestResult TestResult { get; set; }

        public Guid QuestionId { get; set; }
        public Question Question { get; set; }

        public ICollection<Answer> Answers { get; set; }
    
        public TestAnswer()
        {
            Answers = new List<Answer>();
        }
    }
}
