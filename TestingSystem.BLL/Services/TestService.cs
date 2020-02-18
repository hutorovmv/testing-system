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
using TestingSystem.BLL.Utils;

namespace TestingSystem.BLL.Services
{
    public class TestService : ITestService
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserDataService _userDataService;
        private readonly IMapper _mapper;

        public TestService(
            IUnitOfWork uow, 
            IMapper mapper, 
            IUserDataService userDataService)
        {
            _uow = uow;
            _mapper = mapper;
            _userDataService = userDataService;
        }

        public async Task<TestDTO> GetById(Guid testId)
        {
            Test test = await _uow.TestRepository.GetById(testId);
            return _mapper.Map<TestDTO>(test);
        }

        public async Task<PagedList<TestDTO>> GetWithName(string name, int pageSize, int pageIndex)
        {
            PagedList<Test> tests = await _uow.TestRepository.GetWithName(name, pageSize, pageIndex);
            IEnumerable<TestDTO> testDtos = _mapper.Map<IEnumerable<TestDTO>>(tests.Items);
            
            foreach (var item in testDtos)
            {
                UserProfile author = await _uow.UserProfileRepository.GetById(item.AuthorId);
                item.AuthorFullName = $"{author.FirstName} {author.LastName}";
            }

            PagedList<TestDTO> testDtosPagedList = new PagedList<TestDTO>(testDtos, tests.TotalCount, pageSize, pageIndex);
            return testDtosPagedList;
        }

        public async Task<PagedList<TestDTO>> GetWithProperties(string name, string authorFullName, int? timeRequiredFrom, int? timeRequiredTo,
            DateTime? dateTimeFrom, DateTime? dateTimeTo, int pageSize, int pageIndex)
        {
            string authorId = null;
            if (!string.IsNullOrWhiteSpace(authorFullName))
                authorId = (await _userDataService.UserProfilesWithFullName(authorFullName, 1, 1)).Items.FirstOrDefault().Id;

            PagedList<Test> tests = await _uow.TestRepository.GetWithProperties(name, authorId, 
                timeRequiredFrom, timeRequiredTo, dateTimeFrom, dateTimeTo, pageSize, pageIndex);
            return tests.ConvertPagedList<Test, TestDTO>(_mapper);
        }

        public async Task<OperationDetails> Create(TestDTO testDto)
        {
            try
            {
                Test test = _mapper.Map<Test>(testDto);
                _uow.TestRepository.Create(test);
                await _uow.SaveAsync();
            }
            catch (Exception ex)
            {
                return new OperationDetails(false, ex.Message);
            }

            return new OperationDetails(true, "Test created successfuly");
        }

        public async Task<OperationDetails> Update(TestDTO testDto)
        {
            try
            {
                Test test = await _uow.TestRepository.GetById(testDto.Id);
                test = _mapper.Map<TestDTO, Test>(testDto, test);
                _uow.TestRepository.Update(test);
                await _uow.SaveAsync();
            }
            catch (Exception ex)
            {
                return new OperationDetails(false, ex.Message);
            }

            return new OperationDetails(true, "Test updated successfuly");
        }

        public async Task<OperationDetails> DeleteTest(Guid id)
        {
            try
            {
                Test test = await _uow.TestRepository.GetById(id);
                _uow.TestRepository.Delete(test);
                await _uow.SaveAsync();
            }
            catch (Exception ex)
            {
                return new OperationDetails(false, ex.Message);
            }

            return new OperationDetails(true, "Test removed successfuly");
        }

        public void Dispose() => _uow.Dispose();
    }
}
