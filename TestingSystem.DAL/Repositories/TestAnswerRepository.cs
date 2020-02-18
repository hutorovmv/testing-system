using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.DAL.Extensions;
using TestingSystem.DAL.Interfaces;
using TestingSystem.DAL.Context;
using TestingSystem.Models.Entities;

namespace TestingSystem.DAL.Repositories
{
    public class TestAnswerRepository : Repository<TestAnswer>, ITestAnswerRepository
    {
        public TestAnswerRepository(ApplicationContext context) : base(context) { }

        public async Task<IEnumerable<TestAnswer>> GetForTestResult(Guid testResultId)
        {
            return await GetAll().Where(e => e.TestResultId == testResultId).ToListAsync();
        }
    }
}
