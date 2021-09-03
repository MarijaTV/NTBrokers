using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTBrokers.Models
{
    public class RealEstateModel
    {
        public CompanyModel Company { get; set; }
        public List<CompanyModel> Companies { get; set; }
        public ApartmentModel Apartment { get; set; }
        public List<ApartmentModel> Apartments { get; set; }
        public BrokerModel Broker { get; set; }
        public List<BrokerModel> Brokers { get; set; }
        public List<int> BrokersIds { get; set; }

        public SortFilterModel SortFilter { get; set; }
    }
}
