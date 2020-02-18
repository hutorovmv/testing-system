using System;
using System.Threading.Tasks;
using TestingSystem.Models.Entities;

namespace TestingSystem.DAL.Interfaces
{
    public interface ITestRepository : IRepository<Test>
    {
        Task<PagedList<Test>> GetWithName(string name, int pageSize, int pageIndex);
        Task<PagedList<Test>> GetWithProperties(string name, string authorId, int? timeRequiredFrom, int? timeRequiredTo,
            DateTime? dateTimeFrom, DateTime? dateTimeTo, int pageSize, int pageIndex);
    }
}
