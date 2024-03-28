using System;
using System.Collections.Generic;

namespace project_ecommerce_api.Models;

public partial class Brand
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
