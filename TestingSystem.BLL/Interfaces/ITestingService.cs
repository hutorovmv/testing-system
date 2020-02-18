using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestingSystem.BLL.DTO;
using TestingSystem.BLL.Infrastructure;

namespace TestingSystem.BLL.Interfaces
{
    public interface ITestingService : IDisposable
    {
        Task<TestDTO> GetTestData(Guid testId);
        Task<TestResultDTO> StartTesting(Guid testId, string userId);
        Task<OperationDetails> CheckAnswer(Guid testResultId, Guid questionId, IEnumerable<Guid> answerIds);
        Task<TestResultDTO> EndTesting(Guid testResultId);
    }
}
