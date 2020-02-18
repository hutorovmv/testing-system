using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TestingSystem.BLL.Interfaces;
using TestingSystem.BLL.Infrastructure;
using TestingSystem.BLL.DTO;
using TestingSystem.DAL.Interfaces;
using TestingSystem.Models.Entities;

namespace TestingSystem.BLL.Services
{
    public class TestingService : ITestingService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TestingService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<TestDTO> GetTestData(Guid testId)
        {
            Test test = await _uow.TestRepository.GetById(testId);
            TestDTO testDto = _mapper.Map<TestDTO>(test);

            IEnumerable<Question> questions = await _uow.QuestionRepository.GetAllValid(testId);
            IEnumerable<QuestionDTO> questionDtos = _mapper.Map<IEnumerable<QuestionDTO>>(questions);

            foreach (var questionDto in questionDtos)
            {
                IEnumerable<Answer> answers = await _uow.AnswerRepository.GetAll(questionDto.Id);
                IEnumerable<AnswerDTO> answerDtos = _mapper.Map<IEnumerable<AnswerDTO>>(answers);
                questionDto.Answers = answerDtos;
                questionDto.CorrectAnswersNumber = await _uow.AnswerRepository.CorrectAnswersForQuestionCount(questionDto.Id);
            }

            testDto.Questions = questionDtos;
            testDto.QuestionsNumber = testDto.Questions.Count();
            return testDto;
        }

        public async Task<TestResultDTO> StartTesting(Guid testId, string userId)
        {
            TestResult testResult = new TestResult
            {
                TestId = testId,
                UserId = userId,
                StartDateTime = DateTime.UtcNow,
                CorrectAnswers = 0
            };

            _uow.TestResultRepository.Create(testResult);
            await _uow.SaveAsync();

            TestResultDTO testResultDto = _mapper.Map<TestResultDTO>(testResult);
            testResultDto.TotalCorrectAnswers = await _uow.QuestionRepository.CountForTest(testResultDto.TestId);

            return testResultDto;
        }

        public async Task<OperationDetails> CheckAnswer(Guid testResultId, Guid questionId, IEnumerable<Guid> answerIds)
        {
            try
            {
                TestAnswer testAnswer = new TestAnswer
                {
                    TestResultId = testResultId,
                    QuestionId = questionId,
                };

                bool isAllCorrect = true;
                int correct = 0;
                foreach (var id in answerIds)
                {
                    Answer answer = await _uow.AnswerRepository.GetById(id);
                    testAnswer.Answers.Add(answer);
                    if (!answer.IsCorrect)
                        isAllCorrect = false;
                    else
                        correct++;
                }

                int correctSupposedCount = await _uow.AnswerRepository.CorrectAnswersForQuestionCount(questionId);
                if (isAllCorrect && correct == correctSupposedCount)
                    testAnswer.IsCorrect = true;

                _uow.TestAnswerRepository.Create(testAnswer);
                await _uow.SaveAsync();
            }
            catch (Exception ex)
            {
                return new OperationDetails(false, ex.Message);
            }

            return new OperationDetails(true, "Answer checked");
        }

        public async Task<TestResultDTO> EndTesting(Guid testResultId)
        {
            TestResult testResult = await _uow.TestResultRepository.GetById(testResultId);
            IEnumerable<TestAnswer> testAnswers = await _uow.TestAnswerRepository.GetForTestResult(testResultId);

            testResult.CorrectAnswers = testAnswers.Where(e => e.IsCorrect).Count();
            testResult.EndDateTime = DateTime.UtcNow;

            _uow.TestResultRepository.Update(testResult);
            await _uow.SaveAsync();

            TestResultDTO testResultDto = _mapper.Map<TestResultDTO>(testResult);
            testResultDto.TotalCorrectAnswers = await _uow.QuestionRepository.CountForTest(testResult.TestId);

            Test test = await _uow.TestRepository.GetById(testResult.TestId);
            testResultDto.TestName = test.Name;

            if (!string.IsNullOrWhiteSpace(testResult.UserId))
            {
                UserProfile userProfile = await _uow.UserProfileRepository.GetById(testResult.UserId);
                testResultDto.UserFullName = $"{userProfile.FirstName} {userProfile.LastName}";
            }

            return testResultDto;
        }

        public void Dispose() => _uow.Dispose();
    }
}
