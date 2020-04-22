using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        IQueryable<Event> GetAll();

        Task<bool> Exists(int id);
    }
}
