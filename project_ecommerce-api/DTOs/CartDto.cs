namespace project_ecommerce_api.DTOs
{
    public class CartDto
    {
        public Guid CartId { get; set; }
        public Guid CustomerId { get; set; }
        public List<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
    }
}
