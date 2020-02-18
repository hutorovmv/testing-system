using System.Web.Mvc;
using System.Threading.Tasks;
using TestingSystem.BLL.Interfaces;
using TestingSystem.BLL.DTO;
using TestingSystem.BLL.Infrastructure;

namespace TestingSystem.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly IUserManagementService _userManagementService;
        private readonly IUserDataService _userDataService;
        private readonly IMailingService _mailingService;

        public UserController(
            IUserManagementService userManagementService,
            IUserDataService userDataService,
            IMailingService mailingService)
        {
            _userManagementService = userManagementService;
            _userDataService = userDataService;
            _mailingService = mailingService;            
        }

        public async Task<ActionResult> _GiveAdminStatus(string userId)
        {
            OperationDetails result = await _userManagementService.GiveAdminStatus(userId);
            if (result.Succeeded)
            {
                UserDTO userDto = await _userDataService.GetUserInfo(userId);
                await _mailingService.SendEmailAsync(userId, "TestingSystem", "Admin status is given to you!");
                TempData["PartialMessageSuccess"] = result.Message;
            }
            else
                TempData["PartialMessageFailure"] = result.Message;

            return RedirectToAction("_ExtendedAdminUserSearch", "Panel");
        }

        public async Task<ActionResult> _DeleteUser(string userId)
        {
            OperationDetails result = await _userManagementService.DeleteUser(userId);
            if (result.Succeeded)
            {
                UserDTO userDto = await _userDataService.GetUserInfo(userId);
                await _mailingService.SendEmailAsync(userId, "TestingSystem", "Your profile was deleted by admin!");
                TempData["PartialMessageSuccess"] = result.Message;
            }
            else
                TempData["PartialMessageFailure"] = result.Message;

            return RedirectToAction("_ExtendedAdminUserSearch", "Panel");
        }
    }
}