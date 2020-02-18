using System;
using System.Threading.Tasks;
using TestingSystem.BLL.DTO;
using TestingSystem.BLL.Infrastructure;
using TestingSystem.Models.Entities;

namespace TestingSystem.BLL.Interfaces
{
    public interface IUserDataService : IDisposable
    {
        Task<UserDTO> GetUserInfo(string userId);

        Task<PagedList<UserDTO>> UserProfilesWithFullName(string fullName, int pageSize, int pageIndex);
        Task<PagedList<UserDTO>> UserProfilesWithProperties(string firstName, string lastName, 
            string contactEmail, DateTime? birthFrom, DateTime? birthTo, int pageSize, int pageIndex);
        Task<PagedList<UserDTO>> UserProfilesWithPropertiesExtended(string firstName, string lastName,
            string contactEmail, DateTime? birthFrom, DateTime? birthTo, int pageSize, int pageIndex);

        Task<string> GetIdByUserName(string userName);
        Task<OperationDetails> IsUserEmailConfirmed(string userName);

    }
}
