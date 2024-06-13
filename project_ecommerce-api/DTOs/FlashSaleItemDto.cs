namespace project_ecommerce_api.DTOs
{
    public class FlashSaleItemDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public int Discount { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public double PriceSale { get; set; }

        public string DefaultImage { get; set; } = null!;

        public int ColorCount { get; set; }

        public int QuantitySale { get; set; }

        public int QuantitySold { get; set; }

        public string TextUrl { get; set; } = null!;
    }
}
