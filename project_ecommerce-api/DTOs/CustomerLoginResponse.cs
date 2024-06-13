namespace project_ecommerce_api.DTOs
{
    public class CustomerLoginResponse
    {
        public Guid Id { get; set; }

        public Guid CartId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Picture { get; set; } = null!;

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
