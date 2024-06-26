﻿using System;
using System.Collections.Generic;

namespace project_ecommerce_api.Models;

public partial class Category
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? CategoryId { get; set; }

    public int Level { get; set; }

    public string? Icon { get; set; }

    public int Order { get; set; }

    public bool Publish { get; set; }

    public string TextUrl { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
