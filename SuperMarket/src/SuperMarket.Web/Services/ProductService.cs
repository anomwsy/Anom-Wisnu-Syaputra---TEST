using Microsoft.AspNetCore.Mvc.Rendering;
using SuperMarket.Business.Interfaces;
using SuperMarket.Business.Repositories;
using SuperMarket.DataAccess.Models;
using SuperMarket.Web.ViewModels.Products;
using SuperMarket.Web.ViewModels.WareHouses;

namespace SuperMarket.Web.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ProductIndexViewModel GetAllProducts(string name, int pageNumber, int pageSize, int? wareHouseId, bool? expired = null)
        {
            var products = _productRepository.GetProductList(name, wareHouseId, pageNumber, pageSize, expired)
                    .Select(w => new ProductViewModel
                    {
                        Id = w.Id,
                        Name = w.Name,
                        WareHouse = w.WareHouse.Name,
                        IsExpired = w.ExpiredDate < DateTime.Now ? "Expired" : "Not Expired",
                        Price = w.Price?.ToString("C"),
                        Quantity = w.Quantity + " Unit"
                    });
            return new ProductIndexViewModel
            {
                Products = products.ToList(),
                WareHouses = GetWareHouseList(),
                page = pageNumber,
                WareHouseId = wareHouseId,
                IsExpired = expired,
                Name = name,
                totalPage = (int)Math.Ceiling((decimal)_productRepository.GetTotalProduct(name, wareHouseId, expired) / pageSize)
            };
        }

        public List<SelectListItem> GetWareHouseList()
        {
            var wareHouses = _productRepository.GetWareHouseList()
                .Select(w => new SelectListItem
                {
                    Value = w.Id.ToString(),
                    Text = w.Name
                }); ;
            return wareHouses.ToList();
        }

        public ProductUpserViewModel InsertProduct(ProductUpserViewModel vm)
        {
            var newWareHouse = _productRepository.InsertProduct(new Product
            {
                Name = vm.Name,
                Price = vm.Price,
                Quantity = vm.Quantity,
                ExpiredDate = vm.ExpiredDate,
                WareHouseId=vm.WareHouseId,
                DeletedDate = null
            });
            return vm;
        }

        public ProductUpserViewModel UpdateProduct(ProductUpserViewModel vm)
        {
            var wareHouse = _productRepository.GetProductByID(vm.Id);
            if (wareHouse == null)
            {
                throw new KeyNotFoundException("Product Not Found");
            }
            wareHouse.Name = vm.Name;
            wareHouse.Price = vm.Price;
            wareHouse.Quantity = vm.Quantity;
            wareHouse.WareHouseId = vm.WareHouseId;
            var newWareHause = _productRepository.UpdateProduct(wareHouse);
            return vm;
        }

        public ProductDeleteViewModel DeleteWareHause(ProductDeleteViewModel vm)
        {
            var wareHouse = _productRepository.GetProductByID(vm.Id);
            if (wareHouse == null)
            {
                throw new KeyNotFoundException("Product Not Found");
            }
            wareHouse.DeletedDate = DateTime.Now;
            var deletedWareHouse = _productRepository.UpdateProduct(wareHouse);
            return vm;
        }

        public ProductUpserViewModel GetWareHouseById(int? Id)
        {
            var wareHouse = _productRepository.GetProductByID(Id);
            if (wareHouse == null)
            {
                throw new KeyNotFoundException("Product Not Found");
            }
            return new ProductUpserViewModel
            {
                Id = wareHouse.Id,
                Name = wareHouse.Name,
                Price = wareHouse.Price,
                Quantity = wareHouse.Quantity,
                WareHouseId = wareHouse.WareHouseId,
        };
        }

    }
}
