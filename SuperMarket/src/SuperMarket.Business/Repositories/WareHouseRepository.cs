using Microsoft.EntityFrameworkCore;
using SuperMarket.Business.Interfaces;
using SuperMarket.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Business.Repositories
{
    public class WareHouseRepository : IWareHouseRepository
    {
        private readonly SuperMarketContext _dbContext;

        public WareHouseRepository(SuperMarketContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<WareHouse> GetWareHouseList(string name, int page, int pageSize)
        {
            var wareHouses = _dbContext.WareHouses.Where(w =>
                (w.Name.Contains(name) || name == null || name == "")
                &&( w.DeletedDate == null)
                ).Include(w => w.Products);

            return wareHouses.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public int GetTotalWarehouse(string name)
        {
            var total = _dbContext.WareHouses.Where(w =>
                    (w.Name.Contains(name) || name == null || name == "")
                    && (w.DeletedDate == null)
                    ).Include(w => w.Products);
            return total.Count();
        }

        public WareHouse InsertWareHouse(WareHouse wareHouse)
        {
            _dbContext.WareHouses.Add(wareHouse);
            _dbContext.SaveChanges();
            return wareHouse;
        }

        public WareHouse UpdateWareHouse(WareHouse wareHouse)
        {
            _dbContext.WareHouses.Update(wareHouse);
            _dbContext.SaveChanges();
            return wareHouse;
        }

        public WareHouse? GetWareHouseByID(int? id)
        {
            var wareHouse = _dbContext.WareHouses
                .FirstOrDefault(w => w.Id == id);

            return wareHouse;
        }

     
    }
}
