using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchendisersMVC.Domain.DAL.Abstract
{
    public interface IUnitOfWork
    {
        MerchendiserRepository MerchendiserRepository { get; }
        TownRepository TownRepository { get; }
        void Save();
    }
}
