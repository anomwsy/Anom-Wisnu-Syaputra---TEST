using System;
using System.Collections.Generic;

namespace SuperMarket.DataAccess.Models;

public partial class WareHouse
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? DeletedDate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
