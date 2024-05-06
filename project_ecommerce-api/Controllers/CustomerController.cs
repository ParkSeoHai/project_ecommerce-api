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

        public CustomerController(ICustomer customerService)
        {
            this.customerService = customerService;
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
    }
}
