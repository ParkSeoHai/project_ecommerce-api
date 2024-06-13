using project_ecommerce_api.DTOs;

namespace project_ecommerce_api.Repositories.Interface
{
    public interface IProduct
    {
        Task<List<ProductViewDto>> GetProductsAsync();
        Task<List<ProductViewDto>> GetProductsByCategoryAsync(string categoryName, int limit);
        Task<ProductDetailDto> GetProductByTextUrlAsync(string name);
        Task<ProductViewDto?> GetProductByIdAsync(Guid id);
        Task<FlashSaleDto> GetFlashSaleAsync();
    }
}
