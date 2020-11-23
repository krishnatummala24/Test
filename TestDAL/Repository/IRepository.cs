using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using TestDAL.Infrastructure;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace TestDAL.Repository
{
    public interface IRepository<T> where T : class
    {
        IModelContext DB { get; }
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null);
        IEnumerable<T> GetIncluding(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> GetQueryable();
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry Edit(T entity, string[] modifiedProperties);
        void Edit(T entity);
        void Delete(T entity);
        void Save();
        T SaveEntity(T entity, EntityState entityState);

        void Detach(T entity);
    }
}
