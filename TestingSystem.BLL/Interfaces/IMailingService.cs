using System;
using System.Threading.Tasks;
using TestingSystem.BLL.Infrastructure;
using TestingSystem.BLL.DTO;

namespace TestingSystem.BLL.Interfaces
{
    public interface IMailingService : IDisposable
    {
        Task<string> GenerateEmailToken(UserDTO user);
        Task<string> GeneratePasswordToken(UserDTO user);
        Task<OperationDetails> ConfirmEmailAsync(string userId, string code);
        Task<OperationDetails> ResetPassword(string userId, string code, string password);
        Task SendEmailAsync(string userId, string subject, string message);
    }
}
