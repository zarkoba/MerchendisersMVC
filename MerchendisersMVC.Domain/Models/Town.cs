using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MerchendisersMVC.Domain.Models
{
    public class Town
    {
        public int TownId { get; set; }
        [Required(ErrorMessage = "Unos naziva grada je obavezno!")]
        [Remote("IsTownExsist", "Town", AdditionalFields = "originalTownName", ErrorMessage = "Grad se vec nalazi u evidenciji!")]
        public string TownName { get; set; }

        public List<Merchendiser> Merchendisers { get; set; }
    }
}
