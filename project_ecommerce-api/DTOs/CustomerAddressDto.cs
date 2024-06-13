namespace project_ecommerce_api.DTOs
{
    public class CustomerAddressDto
    {
        public Guid Id { get; set; } = Guid.Empty;

        public string CustomerName { get; set; } = null!;

        public string City { get; set; } = null!;

        public string District { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public bool IsDefault { get; set; } = false;
    }
}
