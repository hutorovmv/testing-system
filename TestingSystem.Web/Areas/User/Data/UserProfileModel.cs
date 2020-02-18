using System;
using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Web.Areas.User.Data
{
    public class UserProfileModel
    {
        public string Id { get; set; }

        public byte[] ProfilePhoto { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Contact Email")]
        public string ContactEmail { get; set; }

        [Display(Name = "Contact Phone")]
        public string ContactPhone { get; set; }

        [Display(Name = "About Me")]
        public string AboutMe { get; set; }
    }
}