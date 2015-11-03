using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchendisersMVC.WebUI.Models
{
    public class TownListViewModel
    {
        public int TownId { get; set; }
        public string TownName { get; set; }
        public bool IsChecked { get; set; }
    }
}