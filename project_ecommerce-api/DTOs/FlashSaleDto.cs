namespace project_ecommerce_api.DTOs
{
    public class FlashSaleDto
    {
        public Guid Id { get; set; }

        public int Day { get; set; }

        public int Hour { get; set; }

        public int Minute { get; set; }

        public int Second { get; set; }

        public List<FlashSaleItemDto> FlashSaleItems { get; set; } = new List<FlashSaleItemDto>();
    }
}
