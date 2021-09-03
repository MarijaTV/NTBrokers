using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTBrokers.Models
{
    public class SortFilterModel
    {
        public string sortBy { get; set; }
        public string orderBy { get; set; }
        public int filterByCompany { get; set; }
        public string filterByBroker { get; set; }
        public bool filteByCity { get; set; }
    }
}
