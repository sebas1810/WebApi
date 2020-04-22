using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        async Task<bool> IEventRepository.Exists(int id)
        {
            return await _dbContext.Event.AnyAsync(e => e.Id == id);
        }
    }
}
