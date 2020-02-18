using System;

namespace TestingSystem.BLL.DTO
{
    public class TestResultDTO
    {
        public Guid Id { get; set; }
        
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }

        public int CorrectAnswers { get; set; }
        public int TotalCorrectAnswers { get; set; }

        public string UserId { get; set; }
        public string UserFullName { get; set; }
        
        public Guid TestId { get; set; }
        public string TestName { get; set; }
    }
}
