using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using NTBrokers.Models;

namespace NTBrokers.Services
{
    public class RealEstateDBService
    {
        private ApartmentDBService _apartmentDB;
        private BrokerDBService _brokerDB;
        private CompanyDBService _companyDB;
        private SqlConnection _connection;


        public RealEstateDBService(ApartmentDBService apartmentDB, BrokerDBService brokerDB, CompanyDBService companyDB, SqlConnection connection)
        {
            _apartmentDB = apartmentDB;
            _brokerDB = brokerDB;
            _companyDB = companyDB;
            _connection = connection;
        }

        public RealEstateModel All()
        {
            RealEstateModel realEstate = new()
            {
                Apartments = _apartmentDB.AllApartments(),
                Brokers = _brokerDB.AllBrokers(),
                Companies = _companyDB.AllCompanies(),
            };
            return realEstate;

        }
        public RealEstateModel NewCompany()
        {
            RealEstateModel realEstate = new()
            {
                Brokers = _brokerDB.AllBrokers(),
                Company = new CompanyModel()
            };
            return realEstate;
        }
        public RealEstateModel NewApartment()
        {
            RealEstateModel realEstate = new()
            {
                Companies = _companyDB.AllCompanies(),
                Apartment = new ApartmentModel()
            };
            return realEstate;
        }

        public void AddCompany(RealEstateModel realEstate)
        {
            _companyDB.AddCompany(realEstate);
        }
        //public ApartmentModel BrokerApartments(int brokerid)
        //{
        //    List<ApartmentModel> brokerApartments = new()
        //    {
        //        _apartmentDB.BrokerApartments(brokerid)
        //    };

        //    return View();
        //}
    }
}
