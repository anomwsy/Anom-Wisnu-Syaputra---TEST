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
    public class ProductRepository : IProductRepository
    {
        private readonly SuperMarketContext _dbContext;

        public ProductRepository(SuperMarketContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> GetProductList(string name, int? wareHouse, int page, int pageSize, bool? isExpired)
        {
            var products = _dbContext.Products.Where(p =>
                (p.Name.Contains(name) || name == null || name == "")
                && (p.WareHouseId == wareHouse || wareHouse == null)
                && (isExpired != null ? (isExpired == true ? p.ExpiredDate < DateTime.Now : p.ExpiredDate >= DateTime.Now): true)
                && (p.DeletedDate == null)
                ).Include(p => p.WareHouse);

            return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public int GetTotalProduct(string name, int? wareHouse, bool? isExpired)
        {
            var total = _dbContext.Products.Where(p =>
                (p.Name.Contains(name) || name == null || name == "")
                && (p.WareHouseId == wareHouse || wareHouse == null)
                && (isExpired != null ? (isExpired == true ? p.ExpiredDate < DateTime.Now : p.ExpiredDate >= DateTime.Now) : true)
                && (p.DeletedDate == null)
                ).Include(p => p.WareHouse);
            return total.Count();
        }

        public List<WareHouse> GetWareHouseList()
        {
            var wareHouses = _dbContext.WareHouses.ToList();
            return wareHouses;
        }

        public Product InsertProduct(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return product;
        }

        public Product UpdateProduct(Product product)
        {
            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
            return product;
        }

        public Product? GetProductByID(int? id)
        {
            var product = _dbContext.Products
                .FirstOrDefault(w => w.Id == id);

            return product;
        }
    }
}
