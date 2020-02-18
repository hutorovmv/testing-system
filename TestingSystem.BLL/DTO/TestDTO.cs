using System;
using System.Collections.Generic;

namespace TestingSystem.BLL.DTO
{
    public class TestDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public int? TimeRequired { get; set; }
        public DateTime DateTime { get; set; }

        public string AuthorId { get; set; }
        public string AuthorFullName { get; set; }

        public int QuestionsNumber { get; set; }
        public IEnumerable<QuestionDTO> Questions { get; set; }

        public TestResultDTO TestResult { get; set; }
    }
}
