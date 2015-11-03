using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchendisersMVC.Domain.Models
{
    public class MerchendiserDbContext : DbContext
    {
        public DbSet<Merchendiser> Merchendisers { get; set; }
        public DbSet<Town> Towns { get; set; }

        public MerchendiserDbContext() : base("MerchendisersDb") { }
    }
}
