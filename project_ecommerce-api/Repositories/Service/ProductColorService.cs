using Microsoft.EntityFrameworkCore;
using project_ecommerce_api.Data;
using project_ecommerce_api.DTOs;
using project_ecommerce_api.Repositories.Interface;

namespace project_ecommerce_api.Repositories.Service
{
    public class ProductColorService : IProductColor
    {
        private readonly ApplicationDbContext context;

        public ProductColorService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<ProductColorDto>> GetColorsByProductIdAsync(Guid productId)
        {
            List<ProductColorDto> colorsDto = new List<ProductColorDto>();

            // Query database
            var colors = await context.Colors.Where(c => c.ProductId == productId).ToListAsync();

            foreach (var color in colors)
            {
                ProductColorDto productColorDto = new ProductColorDto()
                {
                    Id = color.Id,
                    Name = color.Name,
                    Image = color.Image,
                    Price = color.Price,
                    Quantity = color.Quantity,
                    ProductId = productId,
                };
                colorsDto.Add(productColorDto);
            }

            return colorsDto;
        }
    }
}
