
namespace TMP.Domain.Interfaces
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository<T> where T : class
    {
        void Create(T entity);
        T Read(int id);                
        void Update(T entity);
        void Delete(T entity);
        
        IQueryable<T> Find(Expression<Func<T, bool>> query);
        IQueryable<T> FindAll();
    }
}
