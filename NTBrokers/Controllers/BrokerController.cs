using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;
using NTBrokers.Models;
using NTBrokers.Services;

namespace NTBrokers.Controllers
{
    public class BrokerController : Controller
    {
        // GET: BrokerController

        private BrokerDBService _brokerDB;
        private ApartmentDBService _apartmentDB;
        private RealEstateDBService _realEstateDB;

        public BrokerController(BrokerDBService brokerDB, ApartmentDBService apartmentDB, RealEstateDBService realEstateDB)
        {
            _brokerDB = brokerDB;
            _apartmentDB = apartmentDB;
            _realEstateDB = realEstateDB;
        }

        public ActionResult Index()
        {
            return View(_brokerDB.AllBrokers());

        }

        // GET: BrokerController/Details/5
        public ActionResult Details(int id)
        {
            var brokerDetailsModel = new BrokerDetailsModel()
            {
                BrokerId = id,
                Apartments = _apartmentDB.BrokerApartments(id)
            };
            return View(brokerDetailsModel);
        }

      
        public ActionResult AssignApartments(int id)
        {
            var brokerDetailsModel = new BrokerDetailsModel()
            {
                BrokerId = id,
                Apartments = _apartmentDB.AllEmptyApartments(id),
                BrokersApartmentsIds = _apartmentDB.BrokerApartments(id).Select(b => b.ApartmentId).ToList()
            };

            return View(brokerDetailsModel);
        }
        [HttpPost]
        public ActionResult AssignApartments(BrokerDetailsModel brokerDetailsModel)
        {
            _apartmentDB.UpdateBrokerApartment(brokerDetailsModel);
            return RedirectToAction("Details", "Broker", brokerDetailsModel.BrokerId);
        }


        /*Apartments = _apartmentDB.AllApartments().Where(x => x.Broker is null).Where(x => x.Company = null).ToList(),*/ //Add filter
                                                                                                                          //BrokersApartmentsIds = _apartmentDB.BrokerApartments(id).Select(b => b.ApartmentId).ToList()

        // GET: BrokerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BrokerController/Create
        [HttpPost]
        public ActionResult Create(BrokerModel broker)
        {
            _brokerDB.AddBroker(broker);
            return RedirectToAction("Index");
        }

        // GET: BrokerController/Edit/5
        public ActionResult Edit(int Id)
        {
            List<BrokerModel> brokers = _brokerDB.AllBrokers();
            BrokerModel broker = brokers.FirstOrDefault(b => b.BrokerId == Id);
            return View(broker);
        }

        // POST: BrokerController/Edit/5
        [HttpPost]
        public ActionResult Edit(BrokerModel broker)
        {
            _brokerDB.UpdateBroker(broker);
            return RedirectToAction("Index");

        }

        // GET: BrokerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BrokerController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            {
                return View();
            }
        }

    }
}

