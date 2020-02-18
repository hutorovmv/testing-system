using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNet.Identity;

namespace TestingSystem.DAL.Identity
{
    public class EmailService : IIdentityMessageService
    {
        private string userName;
        private string password;

        public EmailService(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

        public Task SendAsync(IdentityMessage message)
        {
            string clientName = "smtp.gmail.com";

            SmtpClient client = new SmtpClient(clientName, 587);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(userName, password);
            client.EnableSsl = true;

            var mail = new MailMessage(userName, message.Destination);
            mail.Subject = message.Subject;
            mail.Body = message.Body;
            mail.IsBodyHtml = true;

            return client.SendMailAsync(mail);
        }
    }
}
