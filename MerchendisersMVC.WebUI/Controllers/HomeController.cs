using MerchendisersMVC.Domain.DAL.Abstract;
using MerchendisersMVC.Domain.Models;
using MerchendisersMVC.WebUI.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MerchendisersMVC.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IMerchendiserRepository merchRepository;
        private int pageSize = 5;

        public HomeController(IMerchendiserRepository repo)
        {
            merchRepository = repo;
        }

        // List of Merchendasers
        public ActionResult Index(string sortOrder, int page = 1)
        {
            var merchendisers = merchRepository.GetAllMerchendisers();

            merchendisers = GetSortOrder(sortOrder, merchendisers);  // header sorting  
            PagedList<Merchendiser> model = new PagedList<Merchendiser>(merchendisers, page, pageSize);  // pagination

            return View(model);
        }

        // Information about selected merchendiser
        [Authorize]
        public ActionResult Details(int? id, string returnUrl)
        {
            if (id == null)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Niste zadali JMBG merčendajzera!!!" });
            }

            Merchendiser merchendiser = merchRepository.GetMerchendiserById(id);

            if (merchendiser == null)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Merčendajzer sa zadatim JMBG-om ne postoji u našoj evidenciji. Molimo Vas proverite vrednost JMBG-a i pokušajte ponovo." });
            }

            ViewBag.ReturnUrl = returnUrl;

            return View(merchendiser);
        }

        // Filter merchendisers list by town name
        [Authorize]
        public ActionResult Search(string q, string sortOrder, int page = 1)
        {
            if (q.Trim() == String.Empty)
            {
                return RedirectToAction("Index");
            }

            List<Merchendiser> merchendisers = merchRepository.GetMerchendisersByTownName(q.Trim()).ToList();
            merchendisers = GetSortOrder(sortOrder, merchendisers).ToList();
            PagedList<Merchendiser> model = new PagedList<Merchendiser>(merchendisers, page, pageSize);

            return View("Index", model);
        }

        // ***** METHODS ****** // 

        // header sorting 
        private IEnumerable<Merchendiser> GetSortOrder(string sortOrder, IEnumerable<Merchendiser> merchendisers)
        {
            ViewBag.LastNameSort = String.IsNullOrEmpty(sortOrder) ? "lname_desc" : "";
            ViewBag.MerchIdSort = sortOrder == "mid_asc" ? "mid_desc" : "mid_asc";
            ViewBag.FirstNameSort = sortOrder == "fname_asc" ? "fname_desc" : "fname_asc";

            switch (sortOrder)
            {
                case "lname_desc":
                    merchendisers = merchendisers.OrderByDescending(m => m.LastName);
                    break;
                case "mid_asc":
                    merchendisers = merchendisers.OrderBy(m => m.MerchendiserId);
                    break;
                case "mid_desc":
                    merchendisers = merchendisers.OrderByDescending(m => m.MerchendiserId);
                    break;
                case "fname_asc":
                    merchendisers = merchendisers.OrderBy(m => m.FirstName);
                    break;
                case "fname_desc":
                    merchendisers = merchendisers.OrderByDescending(m => m.FirstName);
                    break;
                default:
                    merchendisers = merchendisers.OrderBy(m => m.LastName);
                    break;
            }
            
            return merchendisers;
        }
    }
}