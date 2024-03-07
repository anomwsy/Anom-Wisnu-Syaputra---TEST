using Microsoft.AspNetCore.Mvc.Rendering;
using SuperMarket.Web.ViewModels.WareHouses;

namespace SuperMarket.Web.ViewModels.Products
{
    public class ProductIndexViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public List<SelectListItem> WareHouses { get; set; }
        public int? WareHouseId { get; set; }
        public bool? IsExpired { get; set; }
        public string Name { get; set; }
        public int page { get; set; }
        public int totalPage { get; set; }
    }
}
