using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestingSystem.Models.Entities;

namespace TestingSystem.DAL.Interfaces
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Task<PagedList<Question>> GetForTest(Guid testId, int pageSize, int pageIndex);
        Task<IEnumerable<Question>> GetAll(Guid testId);
        Task<IEnumerable<Question>> GetAllValid(Guid testId);
        Task<int> CountForTest(Guid testId);
        //Task<IEnumerable<Guid>> GetIdsForTest(Guid testId);
    }
}
