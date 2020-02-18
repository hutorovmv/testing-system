using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestingSystem.Models.Entities;

namespace TestingSystem.DAL.Interfaces
{
    public interface ITestAnswerRepository : IRepository<TestAnswer>
    {
        Task<IEnumerable<TestAnswer>> GetForTestResult(Guid testResultId);
    }
}
