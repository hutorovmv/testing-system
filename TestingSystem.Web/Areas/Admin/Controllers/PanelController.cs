using System;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using AutoMapper;
using TestingSystem.Web.Models;
using TestingSystem.Web.Areas.Admin.Data;
using TestingSystem.BLL.Interfaces;
using TestingSystem.BLL.DTO;
using TestingSystem.Models.Entities;
using TestingSystem.BLL.Utils;

namespace TestingSystem.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class PanelController : Controller
    {
        private PanelItem[] items = new PanelItem[]
        {
            new PanelItem
            {
                Name = "Manage Users",
                Action = "_ExtendedAdminUserSearch",
                Controller = "Panel",
                Area = "Admin",
                UpdateTargetId = "data"
            },
            new PanelItem
            {
                Name = "Manage Tests",
                Action = "_ExtendedAdminTestSearch",
                Controller = "Panel",
                Area = "Admin",
                UpdateTargetId = "data"
            }
        };

        private readonly int pageSize;

        private readonly IUserManagementService _userManagementService;
        private readonly IUserDataService _userDataService;
        private readonly ITestService _testService;
        private readonly IQuestionService _questionService;
        private readonly IAnswerService _answerService;
        private readonly ITestingService _testingService;
        private readonly ITestResultService _testResultService;
        private readonly IAuthenticationManager _authManager;
        private readonly IMapper _mapper;

        public PanelController(
            IUserManagementService userManagementService,
            IUserDataService userDataService,
            ITestService testService,
            IQuestionService questionService,
            IAnswerService answerService,
            ITestingService testingService,
            ITestResultService testResultService,
            IAuthenticationManager authManager,
            IMapper mapper)
        {
            _userManagementService = userManagementService;
            _userDataService = userDataService;
            _testService = testService;
            _questionService = questionService;
            _answerService = answerService;
            _testingService = testingService;
            _testResultService = testResultService;
            _authManager = authManager;
            _mapper = mapper;

            pageSize = Defaults.GetPageSize();
        }

        public ActionResult Index() => View();

        public ActionResult _ShowNavigation() => PartialView("_Navigation", items);

        public async Task<ActionResult> _ExtendedAdminUserSearch(string firstName = "", string lastName = "", 
            string contactEmail = "", DateTime? birthFrom = null, DateTime? birthTo = null, int pageIndex = 1)
        {
            PagedList<UserDTO> userProfileDtos = await _userDataService.UserProfilesWithPropertiesExtended(firstName, lastName, contactEmail, birthFrom, birthTo, pageSize, pageIndex);
            PagedList<UserTableModel> userTableModels = userProfileDtos.ConvertPagedList<UserDTO, UserTableModel>(_mapper);
            return PartialView("_UsersTable", userTableModels);
        }

        public async Task<ActionResult> _ExtendedAdminTestSearch(string name = "", string authorFullName = "",
            int? timeRequiredFrom = null, int? timeRequiredTo = null, DateTime? dateTimeFrom = null, DateTime? dateTimeTo = null, int pageIndex = 1)
        {
            PagedList<TestDTO> testDtos = await _testService.GetWithProperties(name, authorFullName, timeRequiredFrom, timeRequiredTo, dateTimeFrom, dateTimeTo, pageSize, pageIndex);
            PagedList<TestTableModel> testTableModels = testDtos.ConvertPagedList<TestDTO, TestTableModel>(_mapper);
            return PartialView("_TestsTable", testTableModels);
        }

        public async Task<ActionResult> _QuestionsForTest(Guid testId, int pageIndex = 1)
        {
            ViewBag.TestId = testId;
            PagedList<QuestionDTO> questionDtos = await _questionService.GetTestQuestions(testId, pageSize, pageIndex);
            PagedList<QuestionTableModel> questionTableModels = questionDtos.ConvertPagedList<QuestionDTO, QuestionTableModel>(_mapper);
            return PartialView("_QuestionsTable", questionTableModels);
        }

        public async Task<ActionResult> _AnswersForQuestion(Guid questionId, int pageIndex = 1)
        {
            ViewBag.QuestionId = questionId;
            ViewBag.TestId = (await _questionService.GetQuestionById(questionId)).TestId;
            
            PagedList<AnswerDTO> answerDtos = await _answerService.GetQuestionAnswers(questionId, pageSize, pageIndex);
            PagedList<AnswerTableModel> answerTableModels = answerDtos.ConvertPagedList<AnswerDTO, AnswerTableModel>(_mapper);
            return PartialView("_AnswersTable", answerTableModels);
        }

        public async Task<ActionResult> _TestResults(Guid testId, int pageIndex = 1)
        {
            PagedList<TestResultDTO> testingResultDtos = await _testResultService.GetTestResultsForTest(testId, pageSize, pageIndex);
            PagedList<TestResultTableModel> testingResultTableModels = testingResultDtos.ConvertPagedList<TestResultDTO, TestResultTableModel>(_mapper);
            return PartialView("_TestResultsTable", testingResultTableModels);
        }
    }
}