using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using TMP.Domain.Entities.Exercises;
using TMP.Domain.Interfaces;

namespace TMP.DAL.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T>, IDisposable
        where T : class
    {
        protected DbContext Context;
        
        public virtual void Save()
        {
            Context.SaveChanges();
        }

        public virtual void Create(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public virtual IQueryable<T> Find(Expression<Func<T, bool>> query)
        {
            return Context.Set<T>().Where(query);
        }

        public virtual IQueryable<T> FindAll()
        {
            return Context.Set<T>();
        }

        public virtual T Read(int id)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
