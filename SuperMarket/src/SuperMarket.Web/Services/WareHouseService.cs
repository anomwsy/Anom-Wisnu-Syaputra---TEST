using SuperMarket.Business.Interfaces;
using SuperMarket.Web.ViewModels.WareHouses;
using SuperMarket.DataAccess.Models;

namespace SuperMarket.Web.Services
{
    public class WareHouseService
    {
        private readonly IWareHouseRepository _wareHouseRepository;

        public WareHouseService(IWareHouseRepository wareHouseRepository)
        {
            _wareHouseRepository = wareHouseRepository;
        }

        public WareHouseIndexViewModel GetAllWareHouse(string name, int page, int pageSize)
        {
            var warehouses = _wareHouseRepository.GetWareHouseList(name, page, pageSize)
                .Select(w => new WareHouseViiewModel
                {
                    Id = w.Id,
                    Name = w.Name,
                    totalProduct = w.Products.Where(p => p.DeletedDate == null).Count(),
                });
            return new WareHouseIndexViewModel
            {
                WareHouses = warehouses.ToList(),
                page = page,
                name = name,
                totalPage = (int)Math.Ceiling((decimal)_wareHouseRepository.GetTotalWarehouse(name) / pageSize)
            };
        }

        public WareHouseUpsertViewModel GetWareHouseById(int? Id)
        {
            var wareHouse = _wareHouseRepository.GetWareHouseByID(Id);
            if (wareHouse == null)
            {
                throw new KeyNotFoundException("WareHouse Not Found");
            }
            return new WareHouseUpsertViewModel
            {
                Id = wareHouse.Id,
                Name = wareHouse.Name
            };
        }

        public WareHouseUpsertViewModel InsertWarehouse(WareHouseUpsertViewModel vm)
        {
            var newWareHouse = _wareHouseRepository.InsertWareHouse(new WareHouse
            {
                Name = vm.Name,
                DeletedDate = null
            });
            return vm;
        }

        public WareHouseUpsertViewModel UpdateWareHouse(WareHouseUpsertViewModel vm)
        {
            var wareHouse = _wareHouseRepository.GetWareHouseByID(vm.Id);
            if(wareHouse == null)
            {
                throw new KeyNotFoundException("WareHouse Not Found");
            }
            wareHouse.Name = vm.Name;
            var newWareHause = _wareHouseRepository.UpdateWareHouse(wareHouse);
            return vm;
        }

        public WareHouseDeleteViewModel DeleteWareHause(WareHouseDeleteViewModel vm)
        {
            var wareHouse = _wareHouseRepository.GetWareHouseByID(vm.Id);
            if (wareHouse == null)
            {
                throw new KeyNotFoundException("WareHouse Not Found");
            }
            wareHouse.DeletedDate = DateTime.Now;
            var deletedWareHouse = _wareHouseRepository.UpdateWareHouse(wareHouse);
            return vm;
        }


    }
}
