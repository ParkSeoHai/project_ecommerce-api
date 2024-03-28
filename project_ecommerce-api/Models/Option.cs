using System;
using System.Collections.Generic;

namespace project_ecommerce_api.Models;

public partial class Option
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Value { get; set; } = null!;

    public double Price { get; set; }

    public int Quantity { get; set; }

    public Guid ColorId { get; set; }

    public virtual Color Color { get; set; } = null!;

    public virtual ICollection<ProductShop> ProductShops { get; set; } = new List<ProductShop>();
}
