using project_ecommerce_api.Models;

namespace project_ecommerce_api.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string Note { get; set; } = null!;

        public DateTime OrderDate { get; set; }

        public string StatusType { get; set; } = null!;

        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}
