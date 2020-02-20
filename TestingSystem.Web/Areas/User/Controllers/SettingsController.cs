using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using AutoMapper;
using TestingSystem.Web.Models;
using TestingSystem.Web.Areas.User.Data;
using TestingSystem.BLL.Interfaces;
using TestingSystem.BLL.DTO;
using TestingSystem.BLL.Services;
using TestingSystem.Models.Entities;
using System.Configuration;
using TestingSystem.BLL.Utils;
using TestingSystem.BLL.Infrastructure;

namespace TestingSystem.Web.Areas.User.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private PanelItem[] items = new PanelItem[]
        {
            new PanelItem
            {
                Name = "Account Settings",
                Action = "_AccountSettings",
                Controller = "Settings",
                Area = "User"
            },
            new PanelItem
            {
                Name = "Profile settings",
                Action = "_ProfileSettings",
                Controller = "Settings",
                Area = "User"
            },
            new PanelItem
            {
                Name = "Test Results",
                Action = "_TestResults",
                Controller = "Settings",
                Area = "User"
            }
        };

        private string UserId => _authManager.User.Identity.GetUserId();

        private readonly int pageSize;

        private readonly IUserManagementService _userManagementService;
        private readonly IUserDataService _userDataService;
        private readonly ITestResultService _testResultService;
        private readonly IImageService _imageService;
        private readonly IAuthenticationManager _authManager;
        private readonly IMapper _mapper;

        public SettingsController(
            IUserManagementService userManagementService,
            IUserDataService userDataService,
            ITestResultService testResultService,
            IImageService imageService,
            IAuthenticationManager authManager,
            IMapper mapper)
        {
            _userManagementService = userManagementService;
            _userDataService = userDataService;
            _testResultService = testResultService;
            _imageService = imageService;
            _authManager = authManager;
            _mapper = mapper;

            pageSize = Defaults.GetPageSize();
        }

        public ActionResult Index() => View(); 

        public ActionResult _ShowNavigation() => PartialView("_Navigation", items);

        public async Task<ActionResult> _AccountSettings()
        {
            UserDTO user = await _userDataService.GetUserInfo(UserId);
            return PartialView(user);
        }

        public async Task<ActionResult> _ProfileSettings()
        {
            UserDTO userDto = await _userDataService.GetUserInfo(UserId);
            ProfileSettingsModel model = _mapper.Map<ProfileSettingsModel>(userDto);
            return PartialView(model);
        }

        [HttpPost]
        public async Task<ActionResult> _ProfileSettings(ProfileSettingsModel model, HttpPostedFileBase photo)
        {
            if (!ModelState.IsValid)
                return PartialView(model);

            UserDTO userDto = _mapper.Map<UserDTO>(model);
            if (photo != null)
                userDto.ProfilePhoto = await _imageService.GetImageData(photo);
            
            OperationDetails result = await _userManagementService.UpdateUserProfileInfo(UserId, userDto);
            if (result.Succeeded)
                TempData["PartialMessageSuccess"] = result.Message;
            else
                TempData["PartialMessageFailure"] = result.Message;

            return RedirectToAction("_ProfileSettings");
        }

        [HttpPost]
        public async Task<ActionResult> _UpdateEmail(UpdateEmailModel model)
        {
            if (ModelState.IsValid)
            {
                OperationDetails result = await _userManagementService.UpdateUserEmail(UserId, model.Email);
                if (result.Succeeded)
                    TempData["PartialMessage"] = result.Message;
                else
                    TempData["PartialMessage"] = result.Message;
            }
            
            return PartialView(model);
        }

        [HttpPost]
        public async Task<ActionResult> _UpdatePassword(UpdatePasswordModel model)
        {
            if (ModelState.IsValid) 
            { 
                OperationDetails result = await _userManagementService.UpdateUserPassword(UserId, model.Password, model.NewPassword);
                if (result.Succeeded)
                    TempData["PartialMessage"] = result.Message;
                else
                    TempData["PartialMessage"] = result.Message;
            }


            return PartialView(model);
        }

        public async Task<ActionResult> _TestResults(int pageIndex = 1)
        {
            PagedList<TestResultDTO> testingResultDtos = await _testResultService.GetTestResultsForUser(UserId, pageSize, pageIndex);
            PagedList<TestResultTableModel> testingResultTableModels = testingResultDtos.ConvertPagedList<TestResultDTO, TestResultTableModel>(_mapper);
            return PartialView("_TestResultsTable", testingResultTableModels);
        }
    }
}