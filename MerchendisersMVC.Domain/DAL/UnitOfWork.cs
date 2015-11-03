using MerchendisersMVC.Domain.DAL.Abstract;
using MerchendisersMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchendisersMVC.Domain.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private MerchendiserDbContext db;
        private MerchendiserRepository merchendiserRepository;
        private TownRepository townRepository;

        public UnitOfWork()
        {
            db = new MerchendiserDbContext();
        }

        public UnitOfWork(MerchendiserDbContext context)
        {
            db = context;
        }

        public MerchendiserRepository MerchendiserRepository
        {
            get
            {
                if (merchendiserRepository == null)
                {
                    merchendiserRepository = new MerchendiserRepository(db);
                }

                return merchendiserRepository;
            }
        }

        public TownRepository TownRepository
        {
            get
            {
                if (townRepository == null)
                {
                    townRepository = new TownRepository(db);
                }

                return townRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
