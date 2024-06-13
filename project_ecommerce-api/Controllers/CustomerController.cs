using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using project_ecommerce_api.DTOs;
using project_ecommerce_api.Models;
using project_ecommerce_api.Repositories.Interface;

namespace project_ecommerce_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomer customerService;
        private readonly ICart cartService;

        public CustomerController(ICustomer customerService, ICart cartService)
        {
            this.customerService = customerService;
            this.cartService = cartService;
        }
        // Register
        [HttpPost]
        [Route("Register")]
        public async Task<ApiResponse<bool>> Register([FromBody] CustomerRegisterDto customerDto)
        {
            if(customerDto == null)
            {
                return new ApiResponse<bool>(new List<string>() { "Param data is null" });
            }

            // Convert to Customer model
            var customer = new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                Email = customerDto.Email,
                PhoneNumber = customerDto.PhoneNumber,
                Picture = "",
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            // Hash password
            var passwordHasher = new PasswordHasher<Customer>();
            string hashedPassword = passwordHasher.HashPassword(customer, customerDto.Password);
            customer.PasswordHash = hashedPassword;

            var apiResponse = await customerService.RegisterAsync(customer);
            return apiResponse;
        }
        // Login
        [HttpPost]
        [Route("Login")]
        public async Task<ApiResponse<CustomerLoginResponse>> Login([FromBody] CustomerLoginDto customerDto)
        {
            if(customerDto == null)
            {
                return new ApiResponse<CustomerLoginResponse>(new List<string>() { "Param data is null" });
            }

            var apiResponse = await customerService.LoginAsync(customerDto);
            return apiResponse;
        }

        // Get cart
        [HttpGet]
        [Route("GetCartById")]
        public async Task<ApiResponse<CartDto>> GetCartById(Guid id)
        {
            try
            {
                var cartDto = await cartService.GetCartByIdAsync(id);
                return cartDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Add item to cart
        [HttpPost]
        [Route("AddItemCart")]
        public async Task<ApiResponse<bool>> AddItemCart(CartPostDto cartPostDto)
        {
            try
            {
                return await cartService.AddItemCartAsync(cartPostDto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Add item to cart
        [HttpPatch]
        [Route("UpdateItemCart")]
        public async Task<ApiResponse<bool>> UpdateItemCart(CartPostDto cartPostDto)
        {
            try
            {
                return await cartService.UpdateItemCartAsync(cartPostDto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Remove item cart
        [HttpDelete]
        [Route("RemoveItemCart")]
        public async Task<ApiResponse<bool>> RemoveItemCart(Guid cartId, string productUrl)
        {
            try
            {
                return await cartService.RemoveItemCartAsync(cartId, productUrl);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Get info customer
        [HttpGet]
        [Route("GetInfo")]
        public async Task<ApiResponse<CustomerDto>> GetInfoByEmail(string email)
        {
            try
            {
                return await customerService.GetInfo(email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Add new customer address
        [HttpPost]
        [Route("AddCustomerAddress")]
        public async Task<ApiResponse<bool>> AddCustomerAddress(CustomerAddressDto customerAddressDto, string email)
        {
            try
            {
                return await customerService.AddCustomerAddress(customerAddressDto, email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Update customer address
        [HttpPatch]
        [Route("UpdateCustomerAddress")]
        public async Task<ApiResponse<bool>> UpdateCustomerAddress(CustomerAddressDto customerAddressDto, string email)
        {
            try
            {
                return await customerService.UpdateCustomerAddress(customerAddressDto, email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Delete customer address
        [HttpDelete]
        [Route("DeleteCustomerAddress")]
        public async Task<ApiResponse<bool>> DeleteCustomerAddress(Guid addressId)
        {
            try
            {
                return await customerService.DeleteCustomerAddress(addressId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
