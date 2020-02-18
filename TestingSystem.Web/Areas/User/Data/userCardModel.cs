using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Web.Areas.User.Data
{
    public class UserCardModel
    {
        public string Id { get; set; }

        public byte[] ProfilePhoto { get; set; }
        
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public bool IsAdmin { get; set; }
    }
}