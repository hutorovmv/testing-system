using System;
using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Web.Areas.User.Data
{
    public class ProfileSettingsModel
    {
        public byte[] ProfilePhoto { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Contact Email")]
        [EmailAddress]
        public string ContactEmail { get; set; }

        [Display(Name = "Contact Phone")]
        public string ContactPhone { get; set; }

        [Display(Name = "About Me")]
        [DataType(DataType.MultilineText)]
        public string AboutMe { get; set; }
    }
}