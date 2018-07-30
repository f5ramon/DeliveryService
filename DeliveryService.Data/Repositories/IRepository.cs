using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Data.Repositories
{
    public interface IRepository<T> : IDisposable
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
            params string[] includeProperties);
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(object id);
        bool Exists(Expression<Func<T, bool>> filter);
        void Commit();
    }
}
