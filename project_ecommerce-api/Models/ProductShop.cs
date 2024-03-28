using System;
using System.Collections.Generic;

namespace project_ecommerce_api.Models;

public partial class ProductShop
{
    public Guid OptionId { get; set; }

    public Guid AddressShopId { get; set; }

    public int Quantity { get; set; }

    public virtual AddressShop AddressShop { get; set; } = null!;

    public virtual Option Option { get; set; } = null!;
}
