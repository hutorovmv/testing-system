using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Web.Areas.User.Data
{
    public class ForgotPasswordModel
    {
        [Display(Name = "Username")]
        [Required]
        [EmailAddress]
        public string UserName { get; set; }
    }
}