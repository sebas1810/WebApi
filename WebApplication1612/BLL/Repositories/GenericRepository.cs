using DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
           where T : class, new()
    {
        protected WebAppContext _dbContext { get; set; }

        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>().AsQueryable();
        }
        public async Task<T> GetAsync(int id) => await _dbContext.FindAsync<T>(id);

        public IQueryable<T> Query()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public async Task<ICollection<T>> Query(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task InsertAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
        }

    }
}
