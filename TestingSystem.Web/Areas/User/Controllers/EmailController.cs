using System.Web.Mvc;
using System.Threading.Tasks;
using TestingSystem.BLL.Interfaces;
using TestingSystem.BLL.Infrastructure;
using TestingSystem.BLL.DTO;
using TestingSystem.Web.Areas.User.Data;

namespace TestingSystem.Web.Areas.User.Controllers
{
    public class EmailController : Controller
    {
        private readonly IUserDataService _userDataService;
        private readonly IMailingService _mailingService;

        public EmailController(
            IUserDataService userDataService,
            IMailingService mailingService)
        {
            _userDataService = userDataService;
            _mailingService = mailingService;
        }

        public async Task<ActionResult> SendEmailConfirmation(string userName)
        {
            string userId = await _userDataService.GetIdByUserName(userName);
            UserDTO user = await _userDataService.GetUserInfo(userId);

            string code = await _mailingService.GenerateEmailToken(user);
            var url = Url.Action("ConfirmEmail", "Email", new
            {
                area = "User",
                userId = user.Id,
                code = code
            }, protocol: Request.Url.Scheme);
            await _mailingService.SendEmailAsync(user.Id, "TestingSystem", $"<a href='{url}'>Confirm email with this link</a>");

            return View("ConfirmEmail");
        }

        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            OperationDetails result = await _mailingService.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
                return View("EmailSuccessfulyConfirmed");

            return View("ConfirmEmail");
        }

        public ActionResult ForgotPassword() => View(new ForgotPasswordModel());

        [HttpPost]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            OperationDetails result = await _userDataService.IsUserEmailConfirmed(model.UserName);
            if (!result.Succeeded)
                return View("Error", (object)result.Message);

            string userId = await _userDataService.GetIdByUserName(model.UserName);
            UserDTO user = await _userDataService.GetUserInfo(userId);

            string code = await _mailingService.GeneratePasswordToken(user);            
            var url = Url.Action("ResetPassword", "Email", new
            {
                area = "User",
                userId = user.Id,
                code = code
            }, protocol: Request.Url.Scheme);
            await _mailingService.SendEmailAsync(user.Id, "Testing System", $"<a href='{url}'>Reset your password with this link</a>");

            return View("ResetPasswordConfirm", (object)user.Email);
        }

        public ActionResult ResetPassword(string userId, string code)
        {
            ViewBag.UserId = userId;
            ViewBag.Code = code;

            return View(new ResetPasswordModel());
        }

        [HttpPost]
        public async Task<ActionResult> ResetPassword(string userId, string code, ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            OperationDetails result = await _mailingService.ResetPassword(userId, code, model.Password);
            if (!result.Succeeded)
                return View("Error", (object)result.Message);

            return View("PasswordResetSuccessfuly");
        }
    }
}