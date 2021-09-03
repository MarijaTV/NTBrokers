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
    public class CompanyController : Controller
    {
        private CompanyDBService _companyDB;
        private RealEstateDBService _realEstateDB;
        private BrokerDBService _brokerDbService;

        public CompanyController(CompanyDBService companyDB, RealEstateDBService realEstateDB, BrokerDBService brokerDbService)
        {
            _companyDB = companyDB;
            _realEstateDB = realEstateDB;
            _brokerDbService = brokerDbService;
        }

        // GET: CompanyController
        public ActionResult Index()
        {
            var realEstateModel = new RealEstateModel();
            realEstateModel.Companies = _companyDB.AllCompanies();
            return View(realEstateModel);
        }

        // GET: CompanyController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CompanyController/Create
        public ActionResult Create()
        {
            return View(_realEstateDB.NewCompany());
        }

        // POST: CompanyController/Create
        [HttpPost]
        public ActionResult Create(RealEstateModel realEstate)
        {
            _realEstateDB.AddCompany(realEstate);
            return RedirectToAction("Index");

        }

        // GET: CompanyController/Edit/5
        public ActionResult Edit(int Id)
        {
            RealEstateModel realEstateModel = new RealEstateModel();
            //Padaryti selectą iš DB
            realEstateModel.Company = _companyDB.AllCompanies().Where(c => c.CompanyId == Id).FirstOrDefault();
            //Padaryti selectą iš DB
            realEstateModel.BrokersIds = _brokerDbService.GetBrokersByCompany(realEstateModel.Company.CompanyId).Select(x => x.BrokerId).ToList();
            realEstateModel.Brokers = _brokerDbService.AllBrokers();
            return View(realEstateModel);
        }

        // POST: CompanyController/Edit/5
        [HttpPost]
        public ActionResult Edit(RealEstateModel realEstateModel)
        {
            _companyDB.UpdateCompany(realEstateModel);
                return RedirectToAction("Index");
        }

        // GET: CompanyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompanyController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
                return View();
        }
    }
}
