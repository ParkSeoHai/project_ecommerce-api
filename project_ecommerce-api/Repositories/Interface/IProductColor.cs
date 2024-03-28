using project_ecommerce_api.DTOs;

namespace project_ecommerce_api.Repositories.Interface
{
    public interface IProductColor
    {
        // Get all color by product id
        Task<List<ProductColorDto>> GetColorsByProductIdAsync(Guid productId);
    }
}
