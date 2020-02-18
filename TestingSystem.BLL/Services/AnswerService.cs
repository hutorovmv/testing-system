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
    public class AnswerService : IAnswerService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AnswerService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<AnswerDTO> GetAnswerById(Guid answerId)
        {
            Answer answer = await _uow.AnswerRepository.GetById(answerId);
            return _mapper.Map<AnswerDTO>(answer);
        }

        public async Task<PagedList<AnswerDTO>> GetQuestionAnswers(Guid questionId, int pageSize, int pageIndex)
        {
            PagedList<Answer> answers = await _uow.AnswerRepository.GetForQuestion(questionId, pageSize, pageIndex);
            return answers.ConvertPagedList<Answer, AnswerDTO>(_mapper);
        }

        public async Task<OperationDetails> CreateAnswer(AnswerDTO answerDto)
        {
            try
            {
                Answer answer = _mapper.Map<Answer>(answerDto);
                _uow.AnswerRepository.Create(answer);
                await _uow.SaveAsync();
            }
            catch (Exception ex)
            {
                return new OperationDetails(false, ex.Message);
            }

            return new OperationDetails(true, "Answer created successfuly");
        }

        public async Task<OperationDetails> UpdateAnswer(AnswerDTO answerDto)
        {
            try
            {
                Answer answer = _mapper.Map<AnswerDTO, Answer>(answerDto);
                _uow.AnswerRepository.Update(answer);
                await _uow.SaveAsync();
            }
            catch (Exception ex)
            {
                return new OperationDetails(false, ex.Message);
            }

            return new OperationDetails(true, "Answer updated successfuly");
        }

        public async Task<OperationDetails> DeleteAnswer(Guid id)
        {
            try
            {
                Answer answer = await _uow.AnswerRepository.GetById(id);
                _uow.AnswerRepository.Delete(answer);
                await _uow.SaveAsync();
            }
            catch (Exception ex)
            {
                return new OperationDetails(false, ex.Message);
            }

            return new OperationDetails(true, "Answer removed successfuly");
        }

        public void Dispose() => _uow.Dispose();
    }
}
