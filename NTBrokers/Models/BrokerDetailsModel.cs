using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTBrokers.Models
{
    public class BrokerDetailsModel
    {
        public int BrokerId { get; set; }

        public List<ApartmentModel> Apartments { get; set; }
        public List<int> BrokersApartmentsIds { get; set; }
    }
}
