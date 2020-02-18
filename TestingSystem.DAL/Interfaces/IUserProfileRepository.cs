using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestingSystem.Models.Entities;

namespace TestingSystem.DAL.Interfaces
{
    public interface IUserProfileRepository : IRepository<UserProfile>
    {
        Task<PagedList<UserProfile>> GetAllWithFullName(string name, int pageSize, int pageIndex);
        Task<PagedList<UserProfile>> GetAllWithProperties(string firstName, string lastName, string contactEmail, DateTime? birthFrom, DateTime? birthTo, int pageSize, int pageIndex);
        Task<IEnumerable<string>> GetAllWithPropertiesIds(string firstName, string lastName, string contactEmail, DateTime? birthFrom, DateTime? birthTo);
    }
}
