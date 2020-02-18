using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Web.Areas.Admin.Data
{
    public class UserTableModel
    {
        public string Id { get; set; }

        [Display(Name = "Photo")]
        public byte[] ProfilePhoto { get; set; }
        
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }

        public bool IsEmailConfirmed { get; set; }
        public bool IsAdmin => Role == "admin" ? true : false;
    }
}