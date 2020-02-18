using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Web.Areas.User.Data
{
    public class UpdateEmailModel
    {
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
    }
}