using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Web.Areas.User.Data
{
    public class RegisterModel
    {
        [Display(Name = "Email")]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }
    }
}