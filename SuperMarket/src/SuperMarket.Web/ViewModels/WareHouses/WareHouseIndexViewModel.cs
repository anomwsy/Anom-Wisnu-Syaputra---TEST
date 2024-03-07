namespace SuperMarket.Web.ViewModels.WareHouses
{
    public class WareHouseIndexViewModel
    {
        public List<WareHouseViiewModel> WareHouses { get; set;}
        public string name { get; set; }
        public int page { get; set; }
        public int totalPage { get; set; }
    }
}
