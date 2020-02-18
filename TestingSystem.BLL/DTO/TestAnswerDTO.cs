using System;
using System.Collections.Generic;

namespace TestingSystem.BLL.DTO
{
    public class TestAnswerDTO
    {
        public Guid Id { get; set; }
        public bool IsCorrect { get; set; }

        public Guid TestResultId { get; set; }
        public Guid QuestionId { get; set; }

        public ICollection<AnswerDTO> Answers { get; set; }
    }
}
