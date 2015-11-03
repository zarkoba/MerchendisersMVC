using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchendisersMVC.Domain.Models
{
    public class Address
    {
        [Required(ErrorMessage = "Unos naziva ulice je obavezno!")]
        [Display(Name = "Ulica")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Unos broja ulice je obavezno!")]
        [Display(Name = "Broj")]
        [RegularExpression(@"^\d{0,3}$", ErrorMessage = "Broj ulice se može sadržati od jedne do tri cifre i opciono jednog slova mnakon cifre!")]
        public int StreetNo { get; set; }
        [Required(ErrorMessage = "Unos poštanskog broja je obavezno!")]
        [Display(Name = "Poštanski broj")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Poštanski broj mora sadržati 5 cifara!")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Unos naziva mesta je obavezno!")]
        [Display(Name = "Mesto")]
        public string City { get; set; }
    }
}
