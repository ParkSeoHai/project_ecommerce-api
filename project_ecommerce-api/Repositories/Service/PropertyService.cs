using Microsoft.EntityFrameworkCore;
using project_ecommerce_api.DTOs;
using project_ecommerce_api.Models;
using project_ecommerce_api.Repositories.Interface;

namespace project_ecommerce_api.Repositories.Service
{
    public class PropertyService : IProperty
    {
        private readonly ApplicationDbContext context;

        public PropertyService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<PropertyDto>> GetPropertiesByProductIdAsync(Guid productId)
        {
            try
            {
                // Find product
                var product = await context.Products.FindAsync(productId);
                if(product == null)
                {
                    throw new Exception("Product not found");
                }
                // Query properies by product id
                var properties = await context.Properties
                    .Where(p => p.ProductId == product.Id)
                    .ToListAsync();

                var propertiesDto = new List<PropertyDto>();
                foreach (var property in properties)
                {
                    var propertyDto = new PropertyDto()
                    {
                        Id = property.Id,
                        Name = property.Name,
                        Value = property.Value
                    };
                    propertiesDto.Add(propertyDto);
                }
                return propertiesDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
