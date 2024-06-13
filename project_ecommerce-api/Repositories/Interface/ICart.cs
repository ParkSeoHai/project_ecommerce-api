using project_ecommerce_api.DTOs;
using project_ecommerce_api.Models;

namespace project_ecommerce_api.Repositories.Interface
{
    public interface ICart
    {
        Task<bool> CreateCart(Cart cart);
        Task<ApiResponse<CartDto>> GetCartByIdAsync(Guid customerId);
        Task<ApiResponse<bool>> AddItemCartAsync(CartPostDto cartItemPost);
        Task<ApiResponse<bool>> UpdateItemCartAsync(CartPostDto cartItemPost);
        Task<ApiResponse<bool>> RemoveItemCartAsync(Guid cartId, string productUrl);
    }
}
