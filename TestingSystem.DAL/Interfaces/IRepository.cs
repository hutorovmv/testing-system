using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace TestingSystem.DAL.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class 
    {
        Task<T> GetById<TId>(TId id);
        IQueryable<T> GetAll();

        void Create(T item);
        void Update(T item);
        void Delete(T item);
    }
}
