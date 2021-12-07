using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashApi.Models
{
    public class CarWashModifyDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string NameOfOwner { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
    }
}
