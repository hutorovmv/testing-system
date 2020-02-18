using System;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using AutoMapper;
using TestingSystem.Web.Areas.Admin.Data;
using TestingSystem.BLL.Interfaces;
using TestingSystem.BLL.Infrastructure;
using TestingSystem.BLL.DTO;
using TestingSystem.BLL.Utils;
using TestingSystem.Models.Entities;

namespace TestingSystem.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class TestController : Controller
    {
        private string UserId => _authManager.User.Identity.GetUserId();

        private readonly int pageSize;

        private readonly ITestService _testService;
        private readonly IQuestionService _questionService;
        private readonly IAnswerService _answerService;
        private readonly IAuthenticationManager _authManager;
        private readonly IMapper _mapper;

        public TestController(
            ITestService testService,
            IQuestionService questionService,
            IAnswerService answerService,
            IAuthenticationManager authManager,
            IMapper mapper)
        {
            _testService = testService;
            _questionService = questionService;
            _answerService = answerService;
            _authManager = authManager;
            _mapper = mapper;

            pageSize = Defaults.GetPageSize();
        }

        public ActionResult _List(int pageIndex = 1) {
            return PartialView("_TestsTableBody");
        }

        public ActionResult _Create() => PartialView();

        [HttpPost]
        public async Task<ActionResult> _Create(TestEditModel model)
        {
            if (ModelState.IsValid)
            {
                TestDTO testDto = _mapper.Map<TestDTO>(model);
                testDto.AuthorId = UserId;
                testDto.DateTime = DateTime.UtcNow;

                OperationDetails result = await _testService.Create(testDto);
                if (result.Succeeded)
                {
                    TempData["PartialMessageSuccess"] = result.Message;
                    return RedirectToAction("_ExtendedAdminTestSearch", "Panel");
                }
                else
                    ModelState.AddModelError(result.Property, result.Message);
            }

            return PartialView(model);
        }

        public async Task<ActionResult> _EditTest(Guid testId)
        {
            TestDTO testDto = await _testService.GetById(testId);
            TestEditModel model = _mapper.Map<TestEditModel>(testDto);
            return PartialView(model);
        }

        [HttpPost]
        public async Task<ActionResult> _EditTest(TestEditModel model)
        {
            if (ModelState.IsValid)
            {
                TestDTO testDto = _mapper.Map<TestDTO>(model);

                OperationDetails result = await _testService.Update(testDto);
                if (result.Succeeded)
                {
                    TempData["PartialMessageSuccess"] = result.Message;
                    return RedirectToAction("_ExtendedAdminTestSearch", "Panel");
                }
                else
                    ModelState.AddModelError(result.Property, result.Message);
            }

            return PartialView(model);
        }

        public async Task<ActionResult> _EditAnswer(Guid answerId)
        {
            AnswerDTO answerDto = await _answerService.GetAnswerById(answerId);
            AnswerEditModel model = _mapper.Map<AnswerEditModel>(answerDto);
            return PartialView(model);
        }

        [HttpPost]
        public async Task<ActionResult> _EditAnswer(AnswerEditModel model)
        {
            if (ModelState.IsValid)
            {
                AnswerDTO answerDto = _mapper.Map<AnswerDTO>(model);

                OperationDetails result = await _answerService.UpdateAnswer(answerDto);
                if (result.Succeeded)
                {
                    TempData["PartialMessageSuccess"] = result.Message;
                    return RedirectToAction("_AnswersForQuestion", "Panel", new { questionId = answerDto.QuestionId });
                }
                else
                    ModelState.AddModelError(result.Property, result.Message);
            }

            return PartialView(model);
        }

        public ActionResult _CreateQuestion(Guid testId) 
        {
            QuestionEditModel model = new QuestionEditModel { TestId = testId };
            return PartialView(model);
        }

        [HttpPost]
        public async Task<ActionResult> _CreateQuestion(QuestionEditModel model, Guid testId)
        {
            if (ModelState.IsValid)
            {
                QuestionDTO questionDto = _mapper.Map<QuestionDTO>(model);

                OperationDetails result = await _questionService.CreateQuestion(questionDto);
                if (result.Succeeded)
                {
                    TempData["PartialMessageSuccess"] = result.Message;
                    return RedirectToAction("_QuestionsForTest", "Panel", new { testId = questionDto.TestId });
                }
                else
                    ModelState.AddModelError(result.Property, result.Message);
            }

            return PartialView(model);
        }

        public async Task<ActionResult> _EditQuestion(Guid questionId)
        {
            QuestionDTO questionDto = await _questionService.GetQuestionById(questionId);
            QuestionEditModel model = _mapper.Map<QuestionEditModel>(questionDto);
            return PartialView(model);
        }

        [HttpPost]
        public async Task<ActionResult> _EditQuestion(QuestionEditModel model)
        {
            if (ModelState.IsValid)
            {
                QuestionDTO questionDto = _mapper.Map<QuestionDTO>(model);

                OperationDetails result = await _questionService.UpdateQuestion(questionDto);
                if (result.Succeeded)
                {
                    TempData["PartialMessageSuccess"] = result.Message;
                    return RedirectToAction("_QuestionsForTest", "Panel", new { testId = questionDto.TestId });
                }
                else
                    ModelState.AddModelError(result.Property, result.Message);
            }

            return PartialView(model);
        } 

        public ActionResult _CreateAnswer(Guid questionId)
        {
            AnswerEditModel model = new AnswerEditModel { QuestionId = questionId };
            return PartialView(model);
        }

        [HttpPost]
        public async Task<ActionResult> _CreateAnswer(AnswerEditModel model, Guid questionId)
        {
            if (ModelState.IsValid)
            {
                AnswerDTO answerDto = _mapper.Map<AnswerDTO>(model);

                OperationDetails result = await _answerService.CreateAnswer(answerDto);
                if (result.Succeeded)
                {
                    TempData["PartialMessageSuccess"] = result.Message;
                    return RedirectToAction("_AnswersForQuestion", "Panel", new { questionId = answerDto.QuestionId });
                }
                else
                    ModelState.AddModelError(result.Property, result.Message);
            }

            return PartialView(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> _SelectTests(string name = "", int pageIndex = 1)
        {
            PagedList<TestDTO> testDtos = await _testService.GetWithName(name, pageSize, pageIndex);
            PagedList<TestCardModel> testCards = testDtos.ConvertPagedList<TestDTO, TestCardModel>(_mapper);
            return PartialView("_Tests", testCards);
        }

        [AllowAnonymous]
        public async Task<ActionResult> _SelectTestsExtended(string name = "", string authorFullName = "",
            int? timeRequiredFrom = null, int? timeRequiredTo = null, DateTime? dateTimeFrom = null, DateTime? dateTimeTo = null, int pageIndex = 1)
        {
            PagedList<TestDTO> testDtos = await _testService.GetWithProperties(name, authorFullName, timeRequiredFrom, timeRequiredTo, dateTimeFrom, dateTimeTo, pageSize, pageIndex);
            PagedList<TestCardModel> testCards = testDtos.ConvertPagedList<TestDTO, TestCardModel>(_mapper);
            return PartialView("_Tests", testCards);
        }

        public async Task<ActionResult> _DeleteTest(Guid testId)
        {
            OperationDetails result = await _testService.DeleteTest(testId);
            if (result.Succeeded)
                TempData["PartialMessageSuccess"] = result.Message;
            else
                TempData["PartialMessageFailure"] = result.Message;

            return RedirectToAction("_ExtendedAdminTestSearch", "Panel");
        }
        
        public async Task<ActionResult> _DeleteQuestion(Guid questionId, Guid testId)
        {
            OperationDetails result = await _questionService.DeleteQuestion(questionId); ;
            if (result.Succeeded)
                TempData["PartialMessageSuccess"] = result.Message;
            else
                TempData["PartialMessageFailure"] = result.Message;
            
            return RedirectToAction("_QuestionsForTest", "Panel", new { testId = testId });
        }

        public async Task<ActionResult> _DeleteAnswer(Guid answerId, Guid questionId)
        {
            OperationDetails result = await _answerService.DeleteAnswer(answerId);
            if (result.Succeeded)
                TempData["PartialMessageSuccess"] = result.Message;
            else
                TempData["PartialMessageFailure"] = result.Message;
           
            return RedirectToAction("_AnswersForQuestion", "Panel", new { questionId = questionId });
        }
    }
}