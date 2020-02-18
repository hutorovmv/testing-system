using System;
using System.Threading.Tasks;
using TestingSystem.BLL.DTO;
using TestingSystem.BLL.Infrastructure;
using TestingSystem.Models.Entities;

namespace TestingSystem.BLL.Interfaces
{
    public interface ITestService : IDisposable
    {
        Task<TestDTO> GetById(Guid testId);
        Task<PagedList<TestDTO>> GetWithName(string name, int pageSize, int pageIndex);
        Task<PagedList<TestDTO>> GetWithProperties(string name, string authorFullName, int? timeRequiredFrom, int? timeRequiredTo,
            DateTime? dateTimeFrom, DateTime? dateTimeTo, int pageSize, int pageIndex);
        Task<OperationDetails> Create(TestDTO testDto);
        Task<OperationDetails> Update(TestDTO testDto);
        Task<OperationDetails> DeleteTest(Guid id);
    }
}
