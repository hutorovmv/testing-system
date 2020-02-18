using System.Configuration;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using TestingSystem.Models.Entities;

namespace TestingSystem.DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(UserStore<ApplicationUser> store) : base(store) 
        {
            var provider = new DpapiDataProtectionProvider("TestingSystem");
            this.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("UserDataConfirmation"));

            string smtpUserName = ConfigurationManager.AppSettings["smtpUserName"];
            string smtpPassword = ConfigurationManager.AppSettings["smtpPassword"];
            this.EmailService = new EmailService(smtpUserName, smtpPassword);
        }
    }
}
