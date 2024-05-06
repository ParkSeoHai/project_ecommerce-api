using System;
using System.Collections.Generic;

namespace project_ecommerce_api.Models;

public partial class Color
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public double Price { get; set; }

    public int Quantity { get; set; }

    public Guid ProductId { get; set; }

    public string Image { get; set; } = null!;

    public virtual ICollection<Option> Options { get; set; } = new List<Option>();

    public virtual Product Product { get; set; } = null!;
}
