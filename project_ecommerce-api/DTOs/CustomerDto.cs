using project_ecommerce_api.Models;

namespace project_ecommerce_api.DTOs
{
    public class CustomerDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Picture { get; set; } = null!;

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public List<CustomerAddressDto> CustomerAddresses { get; set; } = new List<CustomerAddressDto>();

        public OrderDto? Order { get; set; }
    }
}
