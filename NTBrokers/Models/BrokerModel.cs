using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTBrokers.Models
{
    public class BrokerModel
    {
        public int BrokerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName { get
            {
                return FirstName + " " + LastName;
            } 
        }
    }
}
