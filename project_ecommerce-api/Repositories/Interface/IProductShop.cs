using project_ecommerce_api.DTOs;

namespace project_ecommerce_api.Repositories.Interface
{
    public interface IProductShop
    {
        Task<List<ProductShopDto>> GetProductShopByOptionAsync(Guid optionId);
    }
}
