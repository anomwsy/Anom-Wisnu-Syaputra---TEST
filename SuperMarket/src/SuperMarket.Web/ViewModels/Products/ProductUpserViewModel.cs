using Microsoft.AspNetCore.Mvc.Rendering;
using SuperMarket.DataAccess.Models;

namespace SuperMarket.Web.ViewModels.Products
{
    public class ProductUpserViewModel
    {
        public int? Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal? Price { get; set; }

        public int? Quantity { get; set; }

        public DateTime? ExpiredDate { get; set; }

        public int? WareHouseId { get; set; }

        public List<SelectListItem> WareHouses { get; set; }

    }
}
