using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashApi.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime DateTimeSell { get; set; }
        public string Opinion { get; set; }
        public int IdClient { get; set; }
        public int ServiceId { get; set; }
        public int IdCarWash { get; set; }
    }
}
