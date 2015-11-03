using MerchendisersMVC.Domain.DAL.Abstract;
using MerchendisersMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchendisersMVC.Domain.DAL
{
    public class MerchendiserRepository : IMerchendiserRepository
    {
        private MerchendiserDbContext db;

        public MerchendiserRepository()
        {
            db = new MerchendiserDbContext();
        }

        public MerchendiserRepository(MerchendiserDbContext context)
        {
            db = context;
        }

        public IEnumerable<Merchendiser> GetAllMerchendisers()
        {
            return db.Merchendisers.ToList();
        }

        public IEnumerable<Merchendiser> GetMerchendisersByTownName(string townName)
        {
            return (from m in db.Merchendisers.Include("Towns")
                    where m.Towns.Any(t => t.TownName.ToLower() == townName.ToLower())
                    select m).ToList();
        }

        public Merchendiser GetMerchendiserById(int? merchendiserId)
        {
            return db.Merchendisers.Include("Towns").Where(m => m.MerchendiserId == merchendiserId).SingleOrDefault();
        }

        public bool IsPersonalNoValid(string personalNo)
        {
            return !db.Merchendisers.Any(m => m.PersonalNo == personalNo);
        }

        public void AddMerchendiser(Merchendiser merchendiser)
        {
            db.Merchendisers.Add(merchendiser);
        }

        public void UpdateMerchendiser(Merchendiser merchendiser)
        {
            db.Entry(merchendiser).State = System.Data.Entity.EntityState.Modified;
        }

        public void RemoveMerchendiser(int merchendiserId, out string lastName, out string firstName)
        {
            Merchendiser delMerch = GetMerchendiserById(merchendiserId);
            lastName = delMerch.LastName;
            firstName = delMerch.FirstName;
            db.Merchendisers.Remove(delMerch);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
