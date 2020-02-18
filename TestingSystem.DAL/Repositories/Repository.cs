using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using TestingSystem.DAL.Interfaces;
using TestingSystem.DAL.Context;

namespace TestingSystem.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationContext _context;
        public Repository(ApplicationContext context) => _context = context;
        
        public virtual async Task<T> GetById<TId>(TId id) => await _context.Set<T>().FindAsync(id);
        public virtual IQueryable<T> GetAll() => _context.Set<T>().AsQueryable();

        public virtual void Create(T item) => _context.Set<T>().Add(item);
        public virtual void Update(T item) => _context.SetModified(item);
        public virtual void Delete(T item) => _context.Set<T>().Remove(item);
        public void Dispose() => _context.Dispose();
    }
}
