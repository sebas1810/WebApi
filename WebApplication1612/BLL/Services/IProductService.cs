using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IProductService
    {
        Task<Product> GetByIdAsync(int id);
        Task<List<Product>> GetAllAsync();
        Task CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int? input = 0);
        Task DeleteAsync(Product product);
        Task<bool> Exists(int id);
    }
}
