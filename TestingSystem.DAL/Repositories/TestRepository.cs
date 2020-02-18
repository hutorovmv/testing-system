using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.DAL.Extensions;
using TestingSystem.DAL.Interfaces;
using TestingSystem.DAL.Context;
using TestingSystem.Models.Entities;

namespace TestingSystem.DAL.Repositories
{
    public class TestRepository : Repository<Test>, ITestRepository
    {
        public TestRepository(ApplicationContext context) : base(context) { }

        public async Task<PagedList<Test>> GetWithName(string name, int pageSize, int pageIndex)
        {
            IQueryable<Test> items = GetAll();

            if (!string.IsNullOrWhiteSpace(name))
                items = items.Where(e => e.Name.StartsWith(name));

            items = items.OrderByDescending(p => p.DateTime);
            return await items.ToPagedListAsync(pageSize, pageIndex);
        }

        public async Task<PagedList<Test>> GetWithProperties(string name, string authorId, int? timeRequiredFrom, int? timeRequiredTo, 
            DateTime? dateTimeFrom, DateTime? dateTimeTo, int pageSize, int pageIndex)
        {
            IQueryable<Test> items = GetAll();

            if (!string.IsNullOrWhiteSpace(name))
                items = items.Where(e => e.Name.StartsWith(name));

            if (!string.IsNullOrWhiteSpace(authorId))
                items = items.Where(e => e.AuthorId == authorId);

            if (timeRequiredFrom.HasValue)
                items = items.Where(e => e.TimeRequired.Value >= timeRequiredFrom);

            if (timeRequiredTo.HasValue)
                items = items.Where(e => e.TimeRequired.Value <= timeRequiredTo);

            if (dateTimeFrom.HasValue)
                items = items.Where(e => e.DateTime >= dateTimeFrom);

            if (dateTimeTo.HasValue)
                items = items.Where(e => e.DateTime >= dateTimeTo);

            items = items.OrderByDescending(p => p.DateTime);
            return await items.ToPagedListAsync(pageSize, pageIndex);
        }
    }
}
