using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestingSystem.Models.Entities;

namespace TestingSystem.DAL.Interfaces
{
    public interface ITestResultRepository : IRepository<TestResult>
    {
        Task<IEnumerable<Guid>> GetIdsForUser(string userId);
        Task<IEnumerable<Guid>> GetIdsForTest(Guid testId);
    }
}
