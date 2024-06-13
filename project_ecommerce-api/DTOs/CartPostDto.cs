namespace project_ecommerce_api.DTOs
{
    public class CartPostDto
    {
        public Guid CartId { get; set; }
        public string ProductUrl { get; set; } = null!;
        public string Option { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
