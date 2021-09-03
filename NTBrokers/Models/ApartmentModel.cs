using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTBrokers.Models
{
    public class ApartmentModel
    {
        public int ApartmentId { get; set; }
        public string ApartmentName
        {
            get
            {
                return $"{ApartmentType}, {Street} g. {HouseNo}, {City}";
            }
        }
        public string[] ApartmentTypes { get; set; } = new string[] { "Room", "Flat", "Apartment", "House" };
        public string ApartmentType { get; set; } 
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNo { get; set; }
        public int FloorOf { get; set; }
        public int Floors { get; set; }
        public decimal Area { get; set; }
        public string Company { get; set; }
        public int CompanyId { get; set; }
        public List<CompanyModel> Companies { get; set; }
        public string Broker { get; set; }
        public List<BrokerModel> Brokers { get; set; }
        
    }
}
