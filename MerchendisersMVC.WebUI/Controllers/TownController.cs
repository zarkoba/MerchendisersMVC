using MerchendisersMVC.Domain.DAL.Abstract;
using MerchendisersMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MerchendisersMVC.WebUI.Controllers
{
    [Authorize(Users = "admin@example.com")]
    public class TownController : Controller
    {
        private ITownRepository townRepository;

        public TownController(ITownRepository repo)
        {
            townRepository = repo;
        }

        // Town list
        public ActionResult Index()
        {
            return View();
        }

        // Partial: get town list for Index
        public PartialViewResult RenderTownList()
        {
            List<Town> towns = townRepository.GetAllTowns();
            return PartialView("_TownListPartial", towns);
        }

        // Partial: get create town form for Index
        public PartialViewResult AddTown()
        {
            return PartialView("_AddTownPartial");
        }

        // POST: Add town
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Town town)
        {
            if (!IsTownNameValid(town.TownName))
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                townRepository.AddTown(town);
                townRepository.Save();
                TempData["message"] = String.Format("Grad {0} je uspešno dodat u evidenciju!", town.TownName);

                return RedirectToAction("Index");
            }

            return View("Index", town);
        }

        // Validation: Is town already in evidention
        public bool IsTownNameValid(string townName)
        {
            if (townName == null)
            {
                TempData["message"] = String.Format("Unos naziva grada je obavezan!", townName);
                return false;
            }

            if (!townRepository.IsTownNameValid(townName))
            {
                TempData["message"] = String.Format("Grad {0} se vec nalazi u evidenciji", townName);
                return false;
            }

            return true;
        }

        // Partial: Get update town form for Index 
        public PartialViewResult UpdateTown()
        {
            ViewBag.TownList = new SelectList(townRepository.GetAllTowns(), "TownId", "TownName");
            return PartialView("_UpdateTownList");
        }

        // POST: Update Town
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Town town)
        {
            if (!IsTownNameValid(town.TownName))
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                townRepository.UpdateTown(town);
                townRepository.Save();

                return RedirectToAction("Index");
            }
            
            return View(town);
        }

        // Remote validation method - no matching town name       
        public JsonResult IsTownExsist(string TownName, string originalTownName)
        {
            return Json(townRepository.IsTownNameValid(TownName) || TownName == originalTownName, JsonRequestBehavior.AllowGet);
        }
    }
}