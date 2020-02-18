using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Web.Areas.User.Data
{
    public class LoginModel
    {
        [Display(Name = "Username")]
        [EmailAddress]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}