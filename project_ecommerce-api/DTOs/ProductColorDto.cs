namespace project_ecommerce_api.DTOs
{
    public class ProductColorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public double Price { get; set; }
        public double PriceSale { get; set; }
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public string Image { get; set; } = null!;
        public List<ProductOptionDto> Options { get; set; } = new List<ProductOptionDto>();
    }
}
