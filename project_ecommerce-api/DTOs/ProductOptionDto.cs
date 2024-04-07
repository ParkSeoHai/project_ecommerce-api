namespace project_ecommerce_api.DTOs
{
    public class ProductOptionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;
        public double Price { get; set; }
        public double PriceSale { get; set; }
        public int Quantity { get; set; }
        public List<ProductShopDto> ProductShops { get; set; } = new List<ProductShopDto>();
    }
}
