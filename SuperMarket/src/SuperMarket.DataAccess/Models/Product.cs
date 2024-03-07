using System;
using System.Collections.Generic;

namespace SuperMarket.DataAccess.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal? Price { get; set; }

    public int? Quantity { get; set; }

    public DateTime? ExpiredDate { get; set; }

    public int? WareHouseId { get; set; }

    public DateTime? DeletedDate { get; set; }

    public virtual WareHouse? WareHouse { get; set; }
}
