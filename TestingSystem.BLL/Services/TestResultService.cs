using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TestingSystem.BLL.Interfaces;
using TestingSystem.BLL.DTO;
using TestingSystem.DAL.Extensions;
using TestingSystem.DAL.Interfaces;
using TestingSystem.Models.Entities;

namespace TestingSystem.BLL.Services
{
    public class TestResultService : ITestResultService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TestResultService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<TestResultDTO> GetTestResult(Guid testResultId)
        {
            TestResult testResult = await _uow.TestResultRepository.GetById(testResultId);
            TestResultDTO testResultDto = _mapper.Map<TestResultDTO>(testResult);
            testResultDto.TotalCorrectAnswers = await _uow.QuestionRepository.CountForTest(testResult.TestId);

            Test test = await _uow.TestRepository.GetById(testResultDto.TestId);
            testResultDto.TestName = test.Name;

            if (!string.IsNullOrWhiteSpace(testResult.UserId))
            {
                UserProfile userProfile = await _uow.UserProfileRepository.GetById(testResult.UserId);
                testResultDto.UserFullName = $"{userProfile.FirstName} {userProfile.LastName}";
            }

            return testResultDto;
        }

        public async Task<PagedList<TestResultDTO>> GetTestResultsForUser(string userId, int pageSize, int pageIndex)
        {
            IEnumerable<Guid> testResultIds = await _uow.TestResultRepository.GetIdsForUser(userId);

            List<TestResultDTO> testResultDtos = new List<TestResultDTO>();
            foreach (var id in testResultIds)
                testResultDtos.Add(await GetTestResult(id));

            return testResultDtos.ToPagedList(pageSize, pageIndex);
        }

        public async Task<PagedList<TestResultDTO>> GetTestResultsForTest(Guid testId, int pageSize, int pageIndex)
        {
            IEnumerable<Guid> testResultIds = await _uow.TestResultRepository.GetIdsForTest(testId);

            List<TestResultDTO> testResultDtos = new List<TestResultDTO>();
            foreach (var id in testResultIds)
                testResultDtos.Add(await GetTestResult(id));

            return testResultDtos.ToPagedList(pageSize, pageIndex);
        }

        public void Dispose() => _uow.Dispose();
    }
}
