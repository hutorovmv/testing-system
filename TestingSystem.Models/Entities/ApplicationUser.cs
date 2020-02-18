using Microsoft.AspNet.Identity.EntityFramework;

namespace TestingSystem.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public UserProfile UserProfile { get; set; }
    }
}