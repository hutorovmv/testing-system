using System.Collections.Generic;
using System.Web.Mvc;

namespace TestingSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Categories = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Test", Value = "test" },
                new SelectListItem { Text = "User", Value = "user" }
            };

            return View();
        }

        public ActionResult UserExtendedSearch() => View();

        public ActionResult TestExtendedSearch() => View();

        [HttpPost]
        public ActionResult _Selection(string search, string category, string pageIndex)
        {
            switch (category.ToLower())
            {
                case "user":
                    return RedirectToAction("_SelectUserProfiles", "Home", new { area = "User", name = search, pageIndex = pageIndex });
                case "test":
                    return RedirectToAction("_SelectTests", "Test", new { area = "Admin", name = search, pageIndex = pageIndex });
                default:
                    return RedirectToAction("Index");
            }
        }
    }
}