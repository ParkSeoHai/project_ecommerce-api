using Microsoft.EntityFrameworkCore;
using project_ecommerce_api.Data;
using project_ecommerce_api.DTOs;
using project_ecommerce_api.Repositories.Interface;

namespace project_ecommerce_api.Repositories.Service
{
    public class ProductImageService : IProductImage
    {
        private readonly ApplicationDbContext context;

        public ProductImageService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<ProductImageDto>> GetImagesByProductIdAsync(Guid productId)
        {
			try
			{
                // Query image
                var images = await context.Images
                    .Where(i => i.ProductId == productId)
                    .ToListAsync();
                // Init list
                var imageDtos = new List<ProductImageDto>();

                foreach (var image in images)
                {
                    // Convert to image dto
                    var imageDto = new ProductImageDto()
                    {
                        Id = image.Id,
                        Src = image.Src
                    };
                    imageDtos.Add(imageDto);
                }
                return imageDtos;
            }
			catch (Exception)
			{
				throw;
			}
        }
    }
}
