using System;
using System.Collections.Generic;

namespace project_ecommerce_api.Data;

public partial class Status
{
    public Guid Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
