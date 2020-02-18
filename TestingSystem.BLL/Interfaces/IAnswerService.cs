using System;
using System.Threading.Tasks;
using TestingSystem.BLL.DTO;
using TestingSystem.BLL.Infrastructure;
using TestingSystem.Models.Entities;

namespace TestingSystem.BLL.Interfaces
{
    public interface IAnswerService : IDisposable
    {
        Task<AnswerDTO> GetAnswerById(Guid answerId);
        Task<PagedList<AnswerDTO>> GetQuestionAnswers(Guid questionId, int pageSize, int pageIndex);
        Task<OperationDetails> CreateAnswer(AnswerDTO answerDto);
        Task<OperationDetails> UpdateAnswer(AnswerDTO answerDto);
        Task<OperationDetails> DeleteAnswer(Guid id);
    }
}
