using project_ecommerce_api.DTOs;

namespace project_ecommerce_api.Repositories.Interface
{
    public interface IProperty
    {
        Task<List<PropertyDto>> GetPropertiesByProductIdAsync(Guid productId);
    }
}
