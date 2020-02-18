using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestingSystem.Models.Entities;

namespace TestingSystem.DAL.Interfaces
{
    public interface IAnswerRepository : IRepository<Answer>
    {
        Task<PagedList<Answer>> GetForQuestion(Guid questionId, int pageSize, int pageIndex);
        Task<IEnumerable<Answer>> GetAll(Guid questionId);
        Task<int> AnswersForQuestionCount(Guid questionId);
        Task<int> CorrectAnswersForQuestionCount(Guid questionId);
    }
}
