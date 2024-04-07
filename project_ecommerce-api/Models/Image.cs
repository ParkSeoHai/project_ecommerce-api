using System;
using System.Collections.Generic;

namespace project_ecommerce_api.Data;

public partial class Image
{
    public Guid Id { get; set; }

    public string Src { get; set; } = null!;

    public Guid ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;
}
