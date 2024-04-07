using project_ecommerce_api.Data;
using project_ecommerce_api.DTOs;
using project_ecommerce_api.Repositories.Interface;

namespace project_ecommerce_api.Repositories.Service
{
    public class BrandService : IBrand
    {
        private readonly ApplicationDbContext context;

        public BrandService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<BrandDto> GetBrandByIdAsycn(Guid id)
        {
			try
			{
				// Find brand
				var brand = await context.Brands.FindAsync(id);
                if (brand == null)
                {
                    throw new Exception("Brand not found");
                }
                // Convert to brand dto
                var brandDto = new BrandDto()
                {
                    Id = brand.Id,
                    Name = brand.Name
                };
                return brandDto;
			}
			catch (Exception)
			{
				throw;
			}
        }
    }
}
