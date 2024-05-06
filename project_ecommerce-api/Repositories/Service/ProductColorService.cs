using Microsoft.EntityFrameworkCore;
using project_ecommerce_api.DTOs;
using project_ecommerce_api.Models;
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
            // Find product
            var product = await context.Products.FindAsync(productId);
            if(product == null)
            {
                throw new Exception("Product not found");
            }

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
                // Calc price sale
                double priceSale = productColorDto.Price - (productColorDto.Price * product.Discount / 100);
                productColorDto.PriceSale = priceSale;
                // Add to list
                colorsDto.Add(productColorDto);
            }

            return colorsDto;
        }
    }
}
