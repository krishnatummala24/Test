using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TestDAL.Infrastructure;

namespace TestDAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        
        public IModelContext DB { get; }

        public Repository(IModelContext db)
        {
            DB = db;
        }

        public IEnumerable<T> GetAll()
        {
            return DB.Set<T>().ToList();
        }


        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = DB.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }

        public IEnumerable<T> GetIncluding(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DB.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }

            return query;
        }

        public IQueryable<T> GetQueryable()
        {
            return DB.Set<T>().AsQueryable();
        }

        public void Add(T entity)
        {
            DB.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            entities.ToList().ForEach(Add);
        }

        public EntityEntry Edit(T entity, string[] modifiedProperties)
        {
            DB.Attach(entity);
            EntityEntry dbEntityEntry = DB.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;

            foreach (var prop in dbEntityEntry.Metadata.GetProperties())
            {
                dbEntityEntry.Property(prop.Name).IsModified = modifiedProperties.Contains(prop.Name);
            }
            return dbEntityEntry;
        }

        /// <summary>
        /// Edit entire entity - all properties will be included in update
        /// </summary>
        public void Edit(T entity)
        {
            DB.Update<T>(entity);
        }

        public void Delete(T entity)
        {
            EntityEntry dbEntityEntry = DB.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public void Save()
        {
            DB.SaveChanges();
        }

        public T SaveEntity(T entity, EntityState entityState)
        {
            EntityEntry dbEntityEntry = DB.Entry(entity);
            dbEntityEntry.State = entityState;
            DB.SaveChanges();
            return (T)dbEntityEntry.Entity;
        }

        public void Detach(T entity)
        {
            DB.Entry(entity).State = EntityState.Detached;
        }
    }
}
