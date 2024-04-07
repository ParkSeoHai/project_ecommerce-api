using System;
using System.Collections.Generic;

namespace project_ecommerce_api.Data;

public partial class AddressShop
{
    public Guid Id { get; set; }

    public string NameShop { get; set; } = null!;

    public string City { get; set; } = null!;

    public string District { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Note { get; set; } = null!;

    public string UrlMap { get; set; } = null!;

    public virtual ICollection<ProductShop> ProductShops { get; set; } = new List<ProductShop>();
}
