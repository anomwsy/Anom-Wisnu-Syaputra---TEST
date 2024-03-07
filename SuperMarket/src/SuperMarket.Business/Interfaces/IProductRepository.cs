using Microsoft.EntityFrameworkCore;
using SuperMarket.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Business.Interfaces
{
    public interface IProductRepository
    {
        public List<Product> GetProductList(string name, int? wareHouse, int page, int pageSize, bool? isExpired);
        public int GetTotalProduct(string name, int? wareHouse, bool? isExpired);
        public List<WareHouse> GetWareHouseList();
        public Product InsertProduct(Product product);
        public Product UpdateProduct(Product product);
        public Product? GetProductByID(int? id);
    }
}
