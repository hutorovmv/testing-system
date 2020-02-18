using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Models.Entities;

namespace TestingSystem.DAL.Extensions
{
    public static class PagedListExtensions
    {
        public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> items, int pageSize, int pageIndex)
        {
            int totalCount = items.Count();
            IEnumerable<T> itemsCollection = await items.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
            return new PagedList<T>(itemsCollection, totalCount, pageSize, pageIndex);
        }

        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> items, int pageSize, int pageIndex)
        {
            int totalCount = items.Count();
            IEnumerable<T> itemsCollection = items.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            return new PagedList<T>(itemsCollection, totalCount, pageSize, pageIndex);
        }
    }
}
