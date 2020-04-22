using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public interface IGenericRepository<T>
    {
        Task<T> GetAsync(int id);

        IQueryable<T> Query();

        Task<ICollection<T>> Query(Expression<Func<T, bool>> predicate);

        Task InsertAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}
