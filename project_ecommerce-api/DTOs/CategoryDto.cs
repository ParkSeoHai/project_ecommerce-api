namespace project_ecommerce_api.DTOs
{
    public class CategoryDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? CategoryId { get; set; }

        public int Level { get; set; }
    }
}
