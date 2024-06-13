namespace project_ecommerce_api.DTOs
{
    public class CategoryDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? CategoryId { get; set; }

        public int Level { get; set; }

        public string? Icon { get; set; }

        public int Order { get; set; }

        public bool Publish { get; set; }

        public string TextUrl { get; set; } = null!;
    }
}
