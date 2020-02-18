using System.Linq;
using System.Threading.Tasks;
using TestingSystem.BLL.Interfaces;
using TestingSystem.BLL.Infrastructure;
using TestingSystem.BLL.DTO;
using TestingSystem.DAL.Interfaces;

namespace TestingSystem.BLL.Services
{
    public class MailingService : IMailingService
    {
        private readonly IUnitOfWork _uow;

        public MailingService(IUnitOfWork uow) => _uow = uow;

        public async Task<string> GenerateEmailToken(UserDTO user) 
        {
            return await _uow.UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
        }

        public async Task<string> GeneratePasswordToken(UserDTO user)
        {
            return await _uow.UserManager.GeneratePasswordResetTokenAsync(user.Id);
        }

        public async Task<OperationDetails> ConfirmEmailAsync(string userId, string code)
        {
            var result = await _uow.UserManager.ConfirmEmailAsync(userId, code);
            return new OperationDetails(result.Succeeded, result.Errors.FirstOrDefault());
        }

        public async Task<OperationDetails> ResetPassword(string userId, string code, string password)
        {
            var result = await _uow.UserManager.ResetPasswordAsync(userId, code, password);
            return new OperationDetails(result.Succeeded, result.Errors.FirstOrDefault());
        }

        public async Task SendEmailAsync(string userId, string subject, string message)
        {
            await _uow.UserManager.SendEmailAsync(userId, subject, message);
        }

        public void Dispose() => _uow.Dispose();
    }
}
