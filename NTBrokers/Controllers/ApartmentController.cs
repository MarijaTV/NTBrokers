using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTBrokers.Models;
using NTBrokers.Services;

namespace NTBrokers.Controllers
{
    public class ApartmentController : Controller
    {
        private ApartmentDBService _apartmentDB;
        private CompanyDBService _companyDB;
        private RealEstateDBService _realEstateDB;
        private BrokerDBService _brokerDB;

        public ApartmentController(ApartmentDBService apartmentDB, CompanyDBService companyDB, RealEstateDBService realEstateDB, BrokerDBService brokerDB)
        {
            _apartmentDB = apartmentDB;
            _companyDB = companyDB;
            _realEstateDB = realEstateDB;
            _brokerDB = brokerDB;
        }

        // GET: ApartmentController
        public ActionResult Index()
        {
            return View(_apartmentDB.AllApartments());
        }

        // GET: ApartmentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ApartmentController/Create
        public ActionResult Create()
        {

            return View(_realEstateDB.NewApartment());
        }

        // POST: ApartmentController/Create
        [HttpPost]
        public ActionResult Create(ApartmentModel apartment)
        {
            _apartmentDB.AddApartment(apartment);
            return RedirectToAction("Index");
        }

        // GET: ApartmentController/Edit/5
        public ActionResult Edit(int id)
        {
                RealEstateModel realEstateModel = new RealEstateModel();
                //Padaryti selectą iš DB
                realEstateModel.Apartment = _apartmentDB.GetApartment(id);

                realEstateModel.Companies = _companyDB.AllCompanies();
               //realEstateModel.BrokersIds = _brokerDB.GetBrokersByCompany(realEstateModel.Company.CompanyId).Select(x => x.BrokerId).ToList();
                //realEstateModel.Brokers = _brokerDb.AllBrokers();
                return View(realEstateModel);
        }

        // POST: ApartmentController/Edit/5
        [HttpPost]
        public ActionResult Edit(ApartmentModel apartment)
        {
            _apartmentDB.UpdateApartment(apartment);
            
                return RedirectToAction("Index");
        }

        // GET: ApartmentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ApartmentController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
                return View();
        }
    }
}
