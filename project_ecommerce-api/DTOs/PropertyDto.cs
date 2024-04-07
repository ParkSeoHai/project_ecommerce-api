namespace project_ecommerce_api.DTOs
{
    public class PropertyDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Value { get; set; } = null!;
    }
}
