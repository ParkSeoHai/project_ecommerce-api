using project_ecommerce_api.DTOs;

namespace project_ecommerce_api.Repositories.Interface
{
    public interface IAddressShop
    {
        Task<List<AddressShopDto>> GetAddressShopsAsync();
    }
}
