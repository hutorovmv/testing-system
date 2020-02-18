using System;
using System.Collections.Generic;

namespace TestingSystem.BLL.DTO
{
    public class QuestionDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; }

        public Guid TestId { get; set; }

        public int CorrectAnswersNumber { get; set; }

        public IEnumerable<AnswerDTO> Answers { get; set; }
    }
}
