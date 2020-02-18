using System;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using AutoMapper;
using TestingSystem.Web.Areas.Admin.Data;
using TestingSystem.BLL.Interfaces;
using TestingSystem.BLL.DTO;

namespace TestingSystem.Web.Controllers
{
    public class TestPassingController : Controller
    {
        private string UserId => _authManager.User.Identity.GetUserId();

        private readonly ITestService _testService;
        private readonly ITestingService _testingService;
        private readonly IAuthenticationManager _authManager;
        private readonly IMapper _mapper;

        public TestPassingController(
            ITestService testService,
            ITestingService testingService,
            IAuthenticationManager authManager,
            IMapper mapper)
        {
            _testService = testService;
            _testingService = testingService;
            _authManager = authManager;
            _mapper = mapper;
        }

        public async Task<ActionResult> ShowTestDescription(Guid testId)
        {
            TestDTO testDto = await _testService.GetById(testId);
            TestEditModel model = _mapper.Map<TestEditModel>(testDto);
            return View(model);
        }

        public async Task<ActionResult> StartTesting(Guid testId)
        {
            TestDTO testDto = await _testingService.GetTestData(testId);
            testDto.TestResult = await _testingService.StartTesting(testId, UserId);
            return View(testDto);
        }

        [HttpPost]
        public async Task<ActionResult> CheckAnswer(Guid testResultId, Guid questionId, string[] answerIds)
        {
            await _testingService.CheckAnswer(testResultId, questionId, answerIds?.Select(e => new Guid(e)));
            return new EmptyResult();
        }

        public async Task<ActionResult> _EndTesting(Guid testResultId)
        {
            System.Threading.Thread.Sleep(5000);
            TestResultDTO testResultDto = await _testingService.EndTesting(testResultId);
            return PartialView("_EndTesting", testResultDto);
        }
    }
}