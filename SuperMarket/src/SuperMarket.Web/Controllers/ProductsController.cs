using Microsoft.AspNetCore.Mvc;
using SuperMarket.Web.Services;
using SuperMarket.Web.ViewModels.Products;
using SuperMarket.Web.ViewModels.WareHouses;

namespace SuperMarket.Web.Controllers
{
    [Route("Product")]
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Index(string name, int? wareHouseId, bool? isExpired, int page = 1, int pageSize = 10)
        {
            var vm = _productService.GetAllProducts(name, page, pageSize, wareHouseId, isExpired);
            return View(vm);
        }

        [HttpGet("Upsert")]
        public IActionResult Upsert(int? id)
        {
            try
            {
                var vm = new ProductUpserViewModel { Id = null, Name = "" };
                if (id != null)
                {
                    var wareHouse = _productService.GetWareHouseById(id);
                    vm.Id = wareHouse.Id;
                    vm.Name = wareHouse.Name;
                    vm.Name = wareHouse.Name;
                    vm.Price = wareHouse.Price;
                    vm.Quantity = wareHouse.Quantity;
                    vm.WareHouseId = wareHouse.WareHouseId;
                }
                    vm.WareHouses = _productService.GetWareHouseList();
                return View(vm);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction("Index");
            }
        }

        [HttpPost("Upsert")]
        public IActionResult Upsert(ProductUpserViewModel vm)
        {
            try
            {
                if (vm.Id == null)
                {
                    _productService.InsertProduct(vm);
                }
                else
                {
                    _productService.UpdateProduct(vm);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction("Upsert", vm);
            }
        }

        [HttpPost("Delete")]
        public IActionResult Delete(ProductDeleteViewModel vm)
        {
            try
            {
                var result = _productService.DeleteWareHause(vm);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction("Index");
            }
        }
    }
}
