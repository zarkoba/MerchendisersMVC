using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MerchendisersMVC.Domain.Models
{
    public class Merchendiser
    {
        public int MerchendiserId { get; set; }
        [Required(ErrorMessage = "Unos jedinstvenog maticnog broja je obavezno!")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "Jedinstveni maticni broj mora sadržati tacno 13 cifara!")]
        [Remote("IsPersonalNoExsist", "Merchendiser", AdditionalFields = "originalPersonalNo", ErrorMessage = "Mercendajzer sa unetim JMBG-om vec postoji u bazi podataka")]
        [Display(Name = "JMBG")]
        public string PersonalNo { get; set; }
        [Required(ErrorMessage = "Unos imena je obavezno!")]
        [Display(Name = "Ime")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Unos prezimena je obavezno!")]
        [Display(Name = "Prezime")]
        public string LastName { get; set; }
        [Display(Name = "Adresa")]
        public Address Address { get; set; }
        [Required(ErrorMessage = "Unos indeksa je obavezno!")]
        [Range(0, 10, ErrorMessage = "Index može uzeti jednu vrednost iz domena od 0 do 10!")]
        [Display(Name = "Indeks")]
        public int Index { get; set; }

        public List<Town> Towns { get; set; }

        public Merchendiser()
        {
            Address = new Address();
        }
    }
}
