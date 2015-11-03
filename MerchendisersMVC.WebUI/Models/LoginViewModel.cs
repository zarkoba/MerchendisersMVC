using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MerchendisersMVC.WebUI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Unos e-mail adrese je obavezno!")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Unos lozinke je obavezno!")]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [Display(Name = "Zapamti podatke prijave?")]
        public bool RememberMe { get; set; }
    }
}