using Microsoft.AspNetCore.Mvc;
using SuperMarket.Web.Services;
using SuperMarket.Web.ViewModels.WareHouses;

namespace SuperMarket.Web.Controllers
{
    [Route("WareHouse")]
    public class WareHouseController : Controller
    {
        private readonly WareHouseService _wareHouseService;

        public WareHouseController(WareHouseService wareHouseService)
        {
            _wareHouseService = wareHouseService;
        }

        [HttpGet]
        public IActionResult Index(string name, int page = 1, int pageSize = 10)
        {
            var vm = _wareHouseService.GetAllWareHouse(name, page, pageSize);
            return View(vm);
        }

        [HttpGet("Upsert")]
        public IActionResult Upsert(int? id)
        {
            try
            {

                var vm = new WareHouseUpsertViewModel { Id = null, Name = "" };
                if (id != null)
                {
                    var wareHouse = _wareHouseService.GetWareHouseById(id);
                    vm.Id = wareHouse.Id;
                    vm.Name = wareHouse.Name;
                }
                return View(vm);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction("Index");
            }
        }

        [HttpPost("Upsert")]
        public IActionResult Upsert(WareHouseUpsertViewModel vm)
        {
            try
            {
                if (vm.Id == null)
                {
                    _wareHouseService.InsertWarehouse(vm);
                }
                else
                {
                    _wareHouseService.UpdateWareHouse(vm);
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
        public IActionResult Delete(WareHouseDeleteViewModel vm)
        {
            try
            {
                var result = _wareHouseService.DeleteWareHause(vm);
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
