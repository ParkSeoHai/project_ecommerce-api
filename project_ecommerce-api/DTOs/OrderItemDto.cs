using project_ecommerce_api.Models;

namespace project_ecommerce_api.DTOs
{
    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string DefaultImage { get; set; } = null!;
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string TextUrl { get; set; } = null!;
    }
}
