using System;
using System.Collections.Generic;

namespace project_ecommerce_api.Data;

public partial class FlashSale
{
    public Guid Id { get; set; }

    public int Day { get; set; }

    public int Hour { get; set; }

    public int Minute { get; set; }

    public int Second { get; set; }

    public virtual ICollection<FlashSaleItem> FlashSaleItems { get; set; } = new List<FlashSaleItem>();
}
