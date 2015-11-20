namespace TMP.DAL.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using TMP.Domain.Interfaces;

    public class GenericRepository<C, T> : IRepository<T>
        where T : class
        where C : DbContext, new()
    {
        private C _entities = new C();
        public C Context
        {
            get { return _entities; }
            set { _entities = value; }
        }

        public virtual void Save()
        {
            _entities.SaveChanges();
        }

        #region Interface methods

        public virtual void Create(T entity)
        {
            _entities.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _entities.Set<T>().Remove(entity);
        }

        public virtual IQueryable<T> Find(Expression<Func<T, bool>> query)
        {
            return _entities.Set<T>().Where(query);
        }

        public virtual IQueryable<T> FindAll()
        {
            return _entities.Set<T>();
        }

        public virtual T Read(int id)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }

        #endregion
    }
}
