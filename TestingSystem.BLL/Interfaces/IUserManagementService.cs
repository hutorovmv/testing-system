using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using TestingSystem.BLL.DTO;
using TestingSystem.BLL.Infrastructure;

namespace TestingSystem.BLL.Interfaces
{
    public interface IUserManagementService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        
        Task<OperationDetails> UpdateUserEmail(string userId, string newEmail);
        Task<OperationDetails> UpdateUserPassword(string userId, string oldPassword, string newPassword);
        Task<OperationDetails> UpdateUserProfileInfo(string userId, UserDTO userDto);

        Task<OperationDetails> GiveAdminStatus(string userId);
        Task<OperationDetails> DeleteUser(string userId);

        Task<OperationDetails> SetInitialData(UserDTO userDto, List<string> roles);
    }
}
