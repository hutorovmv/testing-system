using System;
using System.Collections.Generic;

namespace TestingSystem.Models.Entities
{
    public class Answer
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public Guid QuestionId { get; set; }
        public Question Question { get; set; }

        public ICollection<TestAnswer> TestAnswers { get; set; }
    }
}
