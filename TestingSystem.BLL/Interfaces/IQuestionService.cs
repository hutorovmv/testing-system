using System;
using System.Threading.Tasks;
using TestingSystem.BLL.DTO;
using TestingSystem.BLL.Infrastructure;
using TestingSystem.Models.Entities;

namespace TestingSystem.BLL.Interfaces
{
    public interface IQuestionService : IDisposable
    {
        Task<QuestionDTO> GetQuestionById(Guid questionId);
        Task<PagedList<QuestionDTO>> GetTestQuestions(Guid testId, int pageSize, int pageIndex);
        Task<OperationDetails> CreateQuestion(QuestionDTO questionDto);
        Task<OperationDetails> UpdateQuestion(QuestionDTO questionDto);
        Task<OperationDetails> DeleteQuestion(Guid id);
    }
}
