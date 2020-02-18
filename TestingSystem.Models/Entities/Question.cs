using System;
using System.Collections.Generic;

namespace TestingSystem.Models.Entities
{
    public class Question
    {
        public Guid Id { get; set; }
        public string Text { get; set; }

        public Guid TestId { get; set; }
        public Test Test { get; set; }

        public ICollection<Answer> Answers { get; set; }
        public ICollection<TestAnswer> TestAnswers { get; set; }
    }
}
