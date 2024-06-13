namespace project_ecommerce_api.DTOs
{
    public class ProductDetailDto
    {
        public Guid Id { get; set; }
        public BrandDto Brand { get; set; } = new BrandDto();
        public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
        public string Name { get; set; } = null!;
        public string DefaultImage { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public double PriceSale { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
        public string TextUrl { get; set; } = null!;
        public List<ProductImageDto> Images { get; set; } = new List<ProductImageDto>();
        public List<ProductColorDto> Colors { get; set; } = new List<ProductColorDto>();
        public List<PropertyDto> Properties { get; set; } = new List<PropertyDto>();
    }
}
