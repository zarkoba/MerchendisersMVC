using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MerchendisersMVC.WebUI.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Unos e-mail adrese je obavezno!")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Unos lozinke je obavezno!")]
        [StringLength(100, ErrorMessage = "{0} mora sadržati minimum {2} karaktera.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrda lozinke")]
        [Compare("Password", ErrorMessage = "Lozinka i potvrda lozinke nisu identični.")]
        public string ConfirmPassword { get; set; }
    }
}