using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using project_ecommerce_api.DTOs;
using project_ecommerce_api.Models;
using project_ecommerce_api.Repositories.Interface;

namespace project_ecommerce_api.Repositories.Service
{
    public class CustomerService : ICustomer
    {
        private readonly ApplicationDbContext context;

        public CustomerService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<ApiResponse<CustomerLoginResponse>> LoginAsync(CustomerLoginDto customerLoginDto)
        {
            try
            {
                // Find customer
                var customer = await context.Customers
                    .FirstOrDefaultAsync(c => c.Email.Equals(customerLoginDto.Email));
                if (customer == null)
                {
                    return new ApiResponse<CustomerLoginResponse>(new List<string>
                    {
                        "Tài khoản hoặc mật khẩu không đúng"
                    });
                }
                // Verify a password during login
                var passwordHasher = new PasswordHasher<CustomerLoginDto>();
                var passwordVerificationResult = passwordHasher.VerifyHashedPassword(customerLoginDto, customer.PasswordHash, customerLoginDto.Password);
                if (passwordVerificationResult == PasswordVerificationResult.Success)
                {
                    // Password is valid
                    // Convert to customer dto
                    var customerDto = new CustomerLoginResponse()
                    {
                        Id = customer.Id,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Email = customer.Email,
                        Picture = customer.Picture,
                        IsActive = customer.IsActive,
                        CreatedDate = customer.CreatedDate,
                    };
                    // Login success
                    return new ApiResponse<CustomerLoginResponse>(customerDto, "Đăng nhập thành công");
                }

                return new ApiResponse<CustomerLoginResponse>(new List<string>
                {
                    "Tài khoản hoặc mật khẩu không đúng"
                });
            }
            catch (Exception ex)
            {
                return new ApiResponse<CustomerLoginResponse>(new List<string>
                {
                    ex.Message
                });
            }
        }

        public async Task<ApiResponse<bool>> RegisterAsync(Customer customer)
        {
            try
            {
                // Check email is exist in database
                bool emailExist = false;
                var customers = await context.Customers
                    .Where(c => c.Email.Equals(customer.Email))
                    .ToListAsync();
                if(customers.Any())
                {
                    emailExist = true;
                }
                // Email was exist
                if(emailExist)
                {
                    return new ApiResponse<bool>(new List<string>
                    {
                        "Email đã tồn tại"
                    });
                }
                // Email won't exist
                await context.Customers.AddAsync(customer);
                await context.SaveChangesAsync();
                return new ApiResponse<bool>(true, "Đăng ký tài khoản thành công. Vui lòng đăng nhập");
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(new List<string>
                {
                    ex.Message
                });
            }
        }
    }
}
