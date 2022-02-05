using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Country { get; set; }
        public DateTime lastUpdated { get; set; }
        public string lastUpdatedBy { get; set; }
    }
}
