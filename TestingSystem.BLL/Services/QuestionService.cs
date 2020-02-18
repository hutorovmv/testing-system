using System;
using System.Threading.Tasks;
using AutoMapper;
using TestingSystem.BLL.Interfaces;
using TestingSystem.BLL.Infrastructure;
using TestingSystem.BLL.DTO;
using TestingSystem.DAL.Interfaces;
using TestingSystem.Models.Entities;
using TestingSystem.BLL.Utils;

namespace TestingSystem.BLL.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _uow;      
        private readonly IMapper _mapper;

        public QuestionService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<QuestionDTO> GetQuestionById(Guid questionId)
        {
            Question question = await _uow.QuestionRepository.GetById(questionId);
            return _mapper.Map<QuestionDTO>(question);
        }

        public async Task<PagedList<QuestionDTO>> GetTestQuestions(Guid testId, int pageSize, int pageIndex)
        {
            PagedList<Question> questions = await _uow.QuestionRepository.GetForTest(testId, pageSize, pageIndex);
            return questions.ConvertPagedList<Question, QuestionDTO>(_mapper);
        }

        public async Task<OperationDetails> CreateQuestion(QuestionDTO questionDto)
        {
            try
            {
                Question question = _mapper.Map<Question>(questionDto);
                _uow.QuestionRepository.Create(question);
                await _uow.SaveAsync();
            }
            catch (Exception ex)
            {
                return new OperationDetails(false, ex.Message);
            }

            return new OperationDetails(true, "Question created successfuly");
        }

        public async Task<OperationDetails> UpdateQuestion(QuestionDTO questionDto)
        {
            try
            {
                Question question = _mapper.Map<Question>(questionDto);
                _uow.QuestionRepository.Update(question);
                await _uow.SaveAsync();
            }
            catch (Exception ex)
            {
                return new OperationDetails(false, ex.Message);
            }

            return new OperationDetails(true, "Question updated successfuly");
        }

        public async Task<OperationDetails> DeleteQuestion(Guid id)
        {
            try
            {
                Question question = await _uow.QuestionRepository.GetById(id);
                _uow.QuestionRepository.Delete(question);
                await _uow.SaveAsync();
            }
            catch (Exception ex)
            {
                return new OperationDetails(false, ex.Message);
            }

            return new OperationDetails(true, "Question removed successfuly");
        }

        public void Dispose() => _uow.Dispose();
    }
}
