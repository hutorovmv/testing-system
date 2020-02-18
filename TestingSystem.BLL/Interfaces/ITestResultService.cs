using System;
using System.Threading.Tasks;
using TestingSystem.BLL.DTO;
using TestingSystem.Models.Entities;

namespace TestingSystem.BLL.Interfaces
{
    public interface ITestResultService : IDisposable
    {
        Task<TestResultDTO> GetTestResult(Guid testResultId);
        Task<PagedList<TestResultDTO>> GetTestResultsForUser(string userId, int pageSize, int pageIndex);
        Task<PagedList<TestResultDTO>> GetTestResultsForTest(Guid testId, int pageSize, int pageIndex);
    }
}
