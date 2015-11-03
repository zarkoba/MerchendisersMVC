using MerchendisersMVC.Domain.DAL.Abstract;
using MerchendisersMVC.Domain.Models;
using MerchendisersMVC.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MerchendisersMVC.WebUI.Controllers
{
    [Authorize(Users = "admin@example.com")]
    public class MerchendiserController : Controller
    {
        private IUnitOfWork repository;

        public MerchendiserController(IUnitOfWork repo)
        {
            repository = repo;
        }

        // start menu
        public ActionResult Menu()
        {
            return View();
        }

        // List of Merchendisers
        public ActionResult Index()
        {
            List<Merchendiser> merchendisers = repository.MerchendiserRepository.GetAllMerchendisers().OrderBy(m => m.LastName).ToList();

            return View(merchendisers);
        }

        // Information about selected merchendiser
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Niste zadali JMBG mercendajzera!!!" });
            }

            Merchendiser merchendiser = repository.MerchendiserRepository.GetMerchendiserById(id);

            if (merchendiser == null)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Mercendajzer sa zadatim JMBG-om ne postoji u našoj evidenciji. Molimo Vas proverite vrednost JMBG-a i pokušajte ponovo." });
            }

            return View(merchendiser);
        }

        // GET: Create new Merchendiser
        public ActionResult Create()
        {
            Merchendiser merchendiser = new Merchendiser();
            merchendiser.Towns = new List<Town>();

            var townList = GetCheckedTownList(merchendiser);
            ViewBag.TownList = townList;

            return View("Edit", merchendiser);
        }

        // POST: Create new Merchendiser        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="MerchendiserId,PersonalNo,FirstName,LastName,Address,Index")] Merchendiser merchendiser, string[] checkedTowns)
        {
            if (ModelState.IsValid)
            {
                PopulateTownListForMerchendiser(merchendiser, checkedTowns);
                
                repository.MerchendiserRepository.AddMerchendiser(merchendiser);
                repository.Save();
                
                TempData["message"] = String.Format("Mercendajzer {0} {1} je uspešno dodat u evidenciju!", merchendiser.LastName, merchendiser.FirstName);
                
                return RedirectToAction("Index");
            }

            PopulateTownListForMerchendiser(merchendiser, checkedTowns);
            var townList = GetCheckedTownList(merchendiser);
            ViewBag.TownList = townList;
            
            return View("Edit", merchendiser);
        }

        // GET: Edit merchendiser
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Niste zadali JMBG mercendajzera!!!" });
            }

            Merchendiser merchendiser = repository.MerchendiserRepository.GetMerchendiserById(id);

            if (merchendiser == null)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Mercendajzer sa zadatim JMBG-om ne postoji u našoj evidenciji. Molimo Vas proverite vrednost JMBG-a i pokušajte ponovo." });
            }

            var townList = GetCheckedTownList(merchendiser);
            ViewBag.TownList = townList;

            return View(merchendiser);
        }

        // POST: Edit merchendiser
        [HttpPost]        
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? MerchendiserId, string[] checkedTowns)
        {
            Merchendiser merchToUpdate = repository.MerchendiserRepository.GetMerchendiserById(MerchendiserId);

            if (TryUpdateModel(merchToUpdate))
            {
                try
                {
                    UpdateMerchendisersTownList(merchToUpdate, checkedTowns);
                    repository.MerchendiserRepository.UpdateMerchendiser(merchToUpdate);

                    repository.Save();

                    TempData["message"] = String.Format("Podaci o merčendajzeru {0} {1} su uspešno izmenjeni!", merchToUpdate.LastName, merchToUpdate.FirstName);

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Promene nisu sačuvane. Pokušajte ponovo, ukoliko se problemi ponove, obratite se sistem administratoru.");
                }
            }

            var townList = GetCheckedTownList(merchToUpdate);
            ViewBag.TownList = townList;

            return View(merchToUpdate);
        }

        // GET: Delete merchendiser
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Niste zadali JMBG merčendajzera!!!" });
            }
            
            Merchendiser merchendiser = repository.MerchendiserRepository.GetMerchendiserById(id);
            
            if (merchendiser == null)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Merčendajzer sa zadatim JMBG-om ne postoji u našoj evidenciji. Molimo Vas proverite vrednost JMBG-a i pokušajte ponovo." });
            }
            
            return View(merchendiser);
        }

        // POST: Delete merchendiser
        [HttpPost]
        [ValidateAntiForgeryToken, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            string lastName;
            string firstName;
            repository.MerchendiserRepository.RemoveMerchendiser(id, out lastName, out firstName);
            repository.Save();
            TempData["message"] = String.Format("Merčendajzer {0} {1} je uspešno izbrisan iz evidencije!", lastName, firstName);

            return RedirectToAction("Index");
        }

        // ***** METHODS ****** // 

        // preparing for ListBox in view - Merchendisers towns
        // add evry town in list and check only ones that is in merchendisers town list
        private List<TownListViewModel> GetCheckedTownList(Merchendiser merchendiser)
        {
            List<TownListViewModel> townList = new List<TownListViewModel>();
            var merchTowns = merchendiser.Towns.Select(t => t.TownId);

            foreach (Town t in repository.TownRepository.GetAllTowns())
            {
                townList.Add(new TownListViewModel { TownId = t.TownId, TownName = t.TownName, IsChecked = merchTowns.Contains(t.TownId) });
            }

            return townList;
        }

        // get check towns for merchendisers from form and populate Towns property
        private void PopulateTownListForMerchendiser(Merchendiser merchendiser, string[] checkedTowns)
        {
            merchendiser.Towns = new List<Town>();

            if (checkedTowns != null)
            {
                foreach (string id in checkedTowns)
                {
                    Town town = repository.TownRepository.GetTownById(int.Parse(id));
                    merchendiser.Towns.Add(town);
                }
            }
        }

        // Update Merchendiser Town property with checked Towns from form
        private void UpdateMerchendisersTownList(Merchendiser merchToUpdate, string[] checkedTowns)
        {
            if (checkedTowns == null)
            {
                merchToUpdate.Towns = new List<Town>();
                return;
            }

            var checkedTownList = checkedTowns.ToList();
            var merchTowns = merchToUpdate.Towns.Select(t => t.TownId).ToList();

            foreach (Town t in repository.TownRepository.GetAllTowns())
            {
                if (checkedTownList.Contains(t.TownId.ToString()))
                {
                    if (!merchTowns.Contains(t.TownId))
                    {
                        merchToUpdate.Towns.Add(t);
                    }
                }
                else
                {
                    if (merchTowns.Contains(t.TownId))
                    {
                        merchToUpdate.Towns.Remove(t);
                    }
                }
            }
        }

        // Remote validation method - no matching personal no
        public JsonResult IsPersonalNoExsist(string PersonalNo, string originalPersonalNo)
        {
            return Json(repository.MerchendiserRepository.IsPersonalNoValid(PersonalNo) || PersonalNo == originalPersonalNo, JsonRequestBehavior.AllowGet);
        }  
    }
}