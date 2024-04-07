using Microsoft.EntityFrameworkCore;
using project_ecommerce_api.Data;
using project_ecommerce_api.DTOs;
using project_ecommerce_api.Repositories.Interface;

namespace project_ecommerce_api.Repositories.Service
{
    public class ProductOptionService : IProductOption
    {
        private readonly ApplicationDbContext context;

        public ProductOptionService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<ProductOptionDto>> GetOptionsByColorAsync(Guid colorId)
        {
            try
            {
                // Find color
                var color = await context.Colors.FindAsync(colorId);
                if(color == null)
                {
                    throw new Exception("Color not found");
                }
                // Find product
                var product = await context.Products.FindAsync(color.ProductId);

                // Query
                var options = await context.Options
                    .Where(o => o.ColorId == color.Id)
                    .ToListAsync();
                // Init list
                var optionDtos = new List<ProductOptionDto>();

                foreach (var option in options)
                {
                    var optionDto = new ProductOptionDto()
                    {
                        Id = option.Id,
                        Name = option.Name,
                        Price = option.Price,
                        Quantity = option.Quantity,
                        Value = option.Value
                    };
                    // Calc price sale
                    double priceSale = optionDto.Price - (optionDto.Price * product.Discount / 100);
                    optionDto.PriceSale = priceSale;
                    // Add to list
                    optionDtos.Add(optionDto);
                }
                return optionDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
