using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.DAL.Interfaces;
using TestingSystem.DAL.Context;
using TestingSystem.DAL.Extensions;
using TestingSystem.Models.Entities;

namespace TestingSystem.DAL.Repositories
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        public QuestionRepository(ApplicationContext context) : base(context) { }

        public async Task<PagedList<Question>> GetForTest(Guid testId, int pageSize, int pageIndex)
        {
            IQueryable<Question> items = GetAll().Where(e => e.TestId == testId);
            return await items.OrderByDescending(e => e.Id).ToPagedListAsync(pageSize, pageIndex);
        }

        public async Task<IEnumerable<Question>> GetAll(Guid testId)
        {
            IQueryable<Question> items = GetAll().Where(e => e.TestId == testId);
            return await items.ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetAllValid(Guid testId)
        {
            IQueryable<Question> items = GetAll().Where(e => e.TestId == testId && e.Answers.Where(i => i.IsCorrect).Count() > 0);
            return await items.ToListAsync();
        }

        public async Task<int> CountForTest(Guid testId)
        {
            IQueryable<Question> items = GetAll().Where(e => e.TestId == testId);
            return await items.Where(e => e.Answers.Where(i => i.IsCorrect).Count() > 0).CountAsync();
        }

        //public async Task<IEnumerable<Guid>> GetIdsForTest(Guid testId)
        //{
        //    IQueryable<Guid> items = GetAll().Where(e => e.TestId == testId).Select(e => e.Id);
        //    return await items.ToListAsync();
        //}
    }
}
