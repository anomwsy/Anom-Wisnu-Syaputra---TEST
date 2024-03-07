using Microsoft.EntityFrameworkCore;
using SuperMarket.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Business.Interfaces
{
    public interface IWareHouseRepository
    {
        public List<WareHouse> GetWareHouseList(string name, int page, int pageSize);
        public int GetTotalWarehouse(string name);
        public WareHouse InsertWareHouse(WareHouse wareHouse);
        public WareHouse UpdateWareHouse(WareHouse wareHouse);
        public WareHouse? GetWareHouseByID(int? id);

    }
}
