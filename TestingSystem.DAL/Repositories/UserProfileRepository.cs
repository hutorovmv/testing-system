using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using TestingSystem.DAL.Interfaces;
using TestingSystem.DAL.Context;
using TestingSystem.Models.Interfaces;
using TestingSystem.DAL.Extensions;
using TestingSystem.Models.Entities;

namespace TestingSystem.DAL.Repositories
{
    public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(ApplicationContext context) : base(context) { }

        public async Task<PagedList<UserProfile>> GetAllWithFullName(string name, int pageSize, int pageIndex)
        {
            IQueryable<UserProfile> items = GetAll();

            if (!string.IsNullOrWhiteSpace(name))
                items = items.Where(e => e.FirstName.StartsWith(name) || e.LastName.StartsWith(name));

            items = items.OrderBy(p => p.LastName);
            return await items.ToPagedListAsync(pageSize, pageIndex);
        }

        public async Task<IEnumerable<string>> GetAllWithPropertiesIds(string firstName, string lastName,
            string contactEmail, DateTime? birthFrom, DateTime? birthTo)
        {
            IQueryable<UserProfile> items = GetAll();

            if (!string.IsNullOrWhiteSpace(firstName))
                items = items.Where(e => e.FirstName.StartsWith(firstName));

            if (!string.IsNullOrWhiteSpace(lastName))
                items = items.Where(e => e.LastName.StartsWith(lastName));

            if (!string.IsNullOrWhiteSpace(contactEmail))
                items = items.Where(e => e.LastName.StartsWith(contactEmail));

            if (birthFrom != null)
                items = items.Where(e => e.BirthDate >= birthFrom);

            if (birthTo != null)
                items = items.Where(e => e.BirthDate <= birthFrom);

            items = items.OrderBy(p => p.Id);

            return await items.Select(e => e.Id).ToListAsync();
        }

        public async Task<PagedList<UserProfile>> GetAllWithProperties(string firstName, string lastName,
            string contactEmail, DateTime? birthFrom, DateTime? birthTo, int pageSize, int pageIndex)
        {
            IQueryable<UserProfile> items = GetAll();

            if (!string.IsNullOrWhiteSpace(firstName))
                items = items.Where(e => e.FirstName.StartsWith(firstName));

            if (!string.IsNullOrWhiteSpace(lastName))
                items = items.Where(e => e.LastName.StartsWith(lastName));

            if (!string.IsNullOrWhiteSpace(contactEmail))
                items = items.Where(e => e.LastName.StartsWith(contactEmail));

            if (birthFrom != null)
                items = items.Where(e => e.BirthDate >= birthFrom);

            if (birthTo != null)
                items = items.Where(e => e.BirthDate <= birthFrom);

            items = items.OrderBy(p => p.Id);

            return await items.ToPagedListAsync(pageSize, pageIndex);
        }
    }
}
