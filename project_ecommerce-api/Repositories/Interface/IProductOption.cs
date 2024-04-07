using project_ecommerce_api.DTOs;

namespace project_ecommerce_api.Repositories.Interface
{
    public interface IProductOption
    {
        Task<List<ProductOptionDto>> GetOptionsByColorAsync(Guid colorId);
    }
}
