using MerchendisersMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchendisersMVC.Domain.DAL.Abstract
{
    public interface ITownRepository
    {
        List<Town> GetAllTowns();
        Town GetTownById(int? townId);
        bool IsTownNameValid(string TownName);
        void AddTown(Town town);
        void UpdateTown(Town town);
        void Save();
    }
}
