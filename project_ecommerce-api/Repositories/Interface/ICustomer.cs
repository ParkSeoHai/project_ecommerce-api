using project_ecommerce_api.DTOs;
using project_ecommerce_api.Models;

namespace project_ecommerce_api.Repositories.Interface
{
    public interface ICustomer
    {
        Task<ApiResponse<CustomerLoginResponse>> LoginAsync(CustomerLoginDto customerLoginDto);
        Task<ApiResponse<bool>> RegisterAsync(Customer customer);
    }
}
