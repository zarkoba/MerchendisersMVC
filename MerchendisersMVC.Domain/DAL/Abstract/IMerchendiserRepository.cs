using MerchendisersMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchendisersMVC.Domain.DAL.Abstract
{
    public interface IMerchendiserRepository
    {
        IEnumerable<Merchendiser> GetAllMerchendisers();
        IEnumerable<Merchendiser> GetMerchendisersByTownName(string townName);
        Merchendiser GetMerchendiserById(int? merchendiserId);
        bool IsPersonalNoValid(string personalNo);
        void AddMerchendiser(Merchendiser merchendiser);
        void UpdateMerchendiser(Merchendiser merchendiser);
        void RemoveMerchendiser(int merchendiserId, out string lastName, out string firstName);
        void Save();
    }
}
