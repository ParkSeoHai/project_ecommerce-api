using System;
using System.Collections.Generic;

namespace project_ecommerce_api.Data;

public partial class Property
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Value { get; set; } = null!;

    public Guid ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;
}
