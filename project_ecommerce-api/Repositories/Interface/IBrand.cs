using project_ecommerce_api.DTOs;

namespace project_ecommerce_api.Repositories.Interface
{
    public interface IBrand
    {
        Task<BrandDto> GetBrandByIdAsycn(Guid id);
    }
}
