using System;
using System.Web.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using TestingSystem.Web.Areas.User.Data;
using TestingSystem.BLL.Interfaces;
using TestingSystem.BLL.DTO;
using TestingSystem.BLL.Utils;
using TestingSystem.Models.Entities;

namespace TestingSystem.Web.Areas.User.Controllers
{
    public class HomeController : Controller
    {
        private readonly int pageSize;

        private readonly IUserDataService _userDataService;
        private readonly IMapper _mapper;

        public HomeController(
            IUserDataService userDataService, 
            IMapper mapper)
        {
            _userDataService = userDataService;
            _mapper = mapper;

            pageSize = Defaults.GetPageSize();
        }

        public async Task<ActionResult> Index(string userId)
        {
            UserDTO userDto = await _userDataService.GetUserInfo(userId);
            UserProfileModel user = _mapper.Map<UserProfileModel>(userDto);

            if (user != null)
                return View(user);
            else
                return View("Error", (object)"No such user profile!");
        }

        public async Task<ActionResult> _SelectUserProfiles(string name = "", int pageIndex = 1)
        {
            PagedList<UserDTO> userProfileDtos = await _userDataService.UserProfilesWithFullName(name, pageSize, pageIndex);
            PagedList<UserCardModel> userProfileCards = userProfileDtos.ConvertPagedList<UserDTO, UserCardModel>(_mapper);
            return PartialView("_UserProfiles", userProfileCards);
        }

        public async Task<ActionResult> _SelectUserProfilesExtended(string firstName = "", string lastName = "", 
            string contactEmail = "", DateTime? birthFrom = null, DateTime? birthTo = null, int pageIndex = 1)
        {
            PagedList<UserDTO> userProfileDtos = await _userDataService.UserProfilesWithProperties(firstName, lastName, contactEmail, birthFrom, birthTo, pageSize, pageIndex);
            PagedList<UserCardModel> userProfileCards = userProfileDtos.ConvertPagedList<UserDTO, UserCardModel>(_mapper); ;
            return PartialView("_UserProfiles", userProfileCards);
        }
    }
}