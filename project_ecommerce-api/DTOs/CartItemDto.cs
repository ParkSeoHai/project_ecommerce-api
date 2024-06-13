namespace project_ecommerce_api.DTOs
{
    public class CartItemDto
    {
        public string Name { get; set; } = null!;
        public string DefaultImage { get; set; } = null!;
        public string Option { get; set; } = null!;
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string TextUrl { get; set; } = null!;
        public int QuantityMax {  get; set; }
    }
}
