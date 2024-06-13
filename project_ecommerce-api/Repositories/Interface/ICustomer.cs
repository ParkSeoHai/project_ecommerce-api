using project_ecommerce_api.DTOs;
using project_ecommerce_api.Models;

namespace project_ecommerce_api.Repositories.Interface
{
    public interface ICustomer
    {
        Task<ApiResponse<CustomerLoginResponse>> LoginAsync(CustomerLoginDto customerLoginDto);
        Task<ApiResponse<bool>> RegisterAsync(Customer customer);
        Task<ApiResponse<CustomerDto>> GetInfo(string email);
        Task<CustomerDto> GetCustomerByEmail(string email);
        Task<List<CustomerAddressDto>> GetCustomerAddresses(Guid customerId);
        Task<OrderDto?> GetCustomerOrders(Guid customerId);
        Task<ApiResponse<bool>> AddCustomerAddress(CustomerAddressDto address, string email);
        Task<ApiResponse<bool>> UpdateCustomerAddress(CustomerAddressDto address, string email);
        Task<ApiResponse<bool>> DeleteCustomerAddress(Guid customerAddressId);
    }
}
