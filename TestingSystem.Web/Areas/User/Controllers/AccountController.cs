using System.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Security.Claims;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using AutoMapper;
using TestingSystem.Web.Areas.User.Data;
using TestingSystem.BLL.Interfaces;
using TestingSystem.BLL.Infrastructure;
using TestingSystem.BLL.DTO;

namespace TestingSystem.Web.Areas.User.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserManagementService _userManagementService;
        private readonly IUserDataService _userDataService;
        private readonly IImageService _imageService;
        private readonly IAuthenticationManager _authManager;
        private readonly IMapper _mapper;

        public AccountController(
            IUserManagementService userManagementService,
            IUserDataService userDataService,
            IImageService imageService,
            IAuthenticationManager authManager,
            IMapper mapper)
        {
            _userManagementService = userManagementService;
            _userDataService = userDataService;
            _imageService = imageService;
            _authManager = authManager;
            _mapper = mapper;
        }

        public ActionResult Login() => View("~/Areas/User/Views/Account/Login.cshtml");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            await SetInitialDataAsync();

            if (ModelState.IsValid)
            {
                UserDTO userDto = _mapper.Map<UserDTO>(model);
                ClaimsIdentity claim = await _userManagementService.Authenticate(userDto);
                
                if (claim == null)
                    ModelState.AddModelError("", "Login or Password is incorrect!");
                else
                {
                    _authManager.SignOut();
                    _authManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);

                    string id = await _userDataService.GetIdByUserName(userDto.UserName);
                    return RedirectToAction("Index", "Home", new { 
                        area = "User", 
                        userId = id
                    });
                }
            }

            return View(model);
        }

        public ActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            await SetInitialDataAsync();

            if (ModelState.IsValid)
            {
                UserDTO userDto = _mapper.Map<UserDTO>(model);
                userDto.ProfilePhoto = await GetDefaultProfileImage();

                OperationDetails operationDetails = await _userManagementService.Create(userDto);
                
                if (operationDetails.Succeeded)
                    return RedirectToAction("SendEmailConfirmation", "Email", new { 
                        area = "User", 
                        userName = userDto.UserName 
                    });
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }

            return View(model);
        }
        
        public ActionResult Logout()
        {
            _authManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public async Task<ActionResult> _ActiveUser()
        {           
            if (_authManager.User.Identity.IsAuthenticated)
            {
                UserDTO userDto = await _userDataService.GetUserInfo(_authManager.User.Identity.GetUserId());
                UserCardModel model = _mapper.Map<UserCardModel>(userDto);
                return PartialView(model);
            }

            return PartialView("_LoginOrRegister");
        }

        public async Task SetInitialDataAsync()
        {
            await _userManagementService.SetInitialData(new UserDTO
            {
                Email = ConfigurationManager.AppSettings["adminEmail"],
                UserName = ConfigurationManager.AppSettings["adminEmail"],
                Password = ConfigurationManager.AppSettings["adminPassword"],
                Role = "admin",
                FirstName = ConfigurationManager.AppSettings["adminFirstName"],
                LastName = ConfigurationManager.AppSettings["adminLastName"],
                ProfilePhoto = await GetDefaultProfileImage()
            }, new List<string> { "user", "admin" });
        }

        public async Task<byte[]> GetDefaultProfileImage()
        {
            string path = ConfigurationManager.AppSettings["defaultProfilePicture"];
            string fullPath = Server.MapPath($"~/Content/{path}");
            byte[] defaultProfileImage = await _imageService.GetImageDataFromFS(fullPath);
            return defaultProfileImage;
        }
    }
}