
namespace project_ecommerce_api.DTOs
{
    public class ProductViewDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public int Discount { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public double PriceSale { get; set; }

        public string DefaultImage { get; set; } = null!;

        public int ColorCount { get; set; }

        public string TextUrl { get; set; } = null!;
    }
}
