using System;
using System.Collections.Generic;

namespace project_ecommerce_api.Data;

public partial class FlashSaleItem
{
    public Guid FlashSaleId { get; set; }

    public Guid ProductId { get; set; }

    public int Discount { get; set; }

    public int QuantitySale { get; set; }

    public int QuantitySold { get; set; }

    public virtual FlashSale FlashSale { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
