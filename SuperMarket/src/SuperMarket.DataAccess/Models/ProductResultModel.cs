using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DataAccess.Models
{
    public class ProductResultModel
    {
        public int WareHouseId { get; set; }
        public string WareHouseName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
