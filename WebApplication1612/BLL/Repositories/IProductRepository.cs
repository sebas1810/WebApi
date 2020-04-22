using BLL.Repository;
using DAL.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<bool> Exists(int id);

        Task<int> InsertAndDelete(Product product); //just testing something
    }
}
