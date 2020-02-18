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
    public class AnswerRepository : Repository<Answer>, IAnswerRepository
    {
        public AnswerRepository(ApplicationContext context) : base(context) { }

        public async Task<PagedList<Answer>> GetForQuestion(Guid questionId, int pageSize, int pageIndex)
        {
            IQueryable<Answer> items = GetAll().Where(e => e.QuestionId == questionId);
            return await items.OrderByDescending(e => e.Id).ToPagedListAsync(pageSize, pageIndex);
        }

        public async Task<IEnumerable<Answer>> GetAll(Guid questionId)
        {
            IQueryable<Answer> items = GetAll().Where(e => e.QuestionId == questionId);
            return await items.ToListAsync();
        }

        public async Task<int> AnswersForQuestionCount(Guid questionId)
        {
            return await GetAll().Where(e => e.QuestionId == questionId).CountAsync();
        }

        public async Task<int> CorrectAnswersForQuestionCount(Guid questionId)
        {
            return await GetAll().Where(e => e.QuestionId == questionId && e.IsCorrect == true).CountAsync();
        }
    }
}
