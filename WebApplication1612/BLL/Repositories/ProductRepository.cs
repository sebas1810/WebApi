using BLL.Repository;
using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(WebAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        async Task<bool> IProductRepository.Exists(int id)
        {
            return await _dbContext.Product.AnyAsync(e => e.Id == id);
        }

        async Task<int> IProductRepository.InsertAndDelete(Product product)
        {
            try
            {
                _dbContext.Set<Product>().Add(product);

                _dbContext.Set<Product>().Remove(product);

                return await _dbContext.SaveChangesAsync();
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
    }
}
