using MerchendisersMVC.WebUI.Infrastructure;
using MerchendisersMVC.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace MerchendisersMVC.WebUI.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        // GET: Register form
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: Register Account
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {                
                try
                {
                    WebSecurity.CreateUserAndAccount(model.Email, model.Password);
                    WebSecurity.Login(model.Email, model.Password);
                    return RedirectToAction("Index", "Home");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", "Došlo je do neočekivane greške. Registracija korisnika nije uspela. Molimo Vas pokušajte ponovo. Ukoliko se problem nastavi pojavljivati pozovitr sistem administratora.");
                }
            }            
            return View(model);
        }

        // GET: Login form
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.Email, model.Password, persistCookie: model.RememberMe))
            {
                return RedirectToLocal(returnUrl);
            }
            
            ModelState.AddModelError("", "Email ili lozinka nisu ispravni. Molimo Vas pokušajte ponovo.");
            return View(model);
        }

        // POST: Logout
        public ActionResult Logoff() 
        {        
            WebSecurity.Logout(); 	
            return RedirectToAction("Index", "Home"); 
        }

        // redirect to return URL and if not local to Home page
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}