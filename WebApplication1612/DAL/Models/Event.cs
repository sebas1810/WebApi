using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Event
    {
        public int Id { get; set; }
        public Guid EventCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public string EventType { get; set; }
        public long UserId { get; set; }
    }
}
