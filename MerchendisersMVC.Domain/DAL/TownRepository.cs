using MerchendisersMVC.Domain.DAL.Abstract;
using MerchendisersMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchendisersMVC.Domain.DAL
{
    public class TownRepository : ITownRepository
    {
        private MerchendiserDbContext db;

        public TownRepository()        
        { 
            db = new MerchendiserDbContext();
        }

        public TownRepository(MerchendiserDbContext context)
        {
            db = context;
        }
                
        public List<Town> GetAllTowns()
        {
            return db.Towns.OrderBy(t => t.TownName).ToList();
        }

        public Town GetTownById(int? townId)
        {
            return db.Towns.Find(townId);
        }

        public void AddTown(Town town)
        {
            db.Towns.Add(town);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public bool IsTownNameValid(string TownName)
        {
            return !db.Towns.Any(t => t.TownName.ToLower() == TownName.ToLower());
        }

        public void UpdateTown(Town town)
        {
            db.Entry(town).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
