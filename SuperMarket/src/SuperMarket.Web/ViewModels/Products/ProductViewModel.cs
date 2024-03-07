namespace SuperMarket.Web.ViewModels.Products
{
    public class ProductViewModel
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string WareHouse { get; set; }
        public string IsExpired { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }
    }
}
