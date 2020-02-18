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
    public class TestResultRepository : Repository<TestResult>, ITestResultRepository
    {
        public TestResultRepository(ApplicationContext context) : base(context) { }

        public async Task<IEnumerable<Guid>> GetIdsForUser(string userId)
        {
            return await GetAll().Where(e => e.UserId == userId && e.EndDateTime.HasValue).OrderByDescending(e => e.EndDateTime).Select(e => e.Id).ToListAsync();
        }

        public async Task<IEnumerable<Guid>> GetIdsForTest(Guid testId)
        {
            return await GetAll().Where(e => e.TestId == testId && e.EndDateTime.HasValue).OrderBy(e => e.CorrectAnswers).ThenByDescending(e => e.EndDateTime).Select(e => e.Id).ToListAsync();
        }
    }
}
