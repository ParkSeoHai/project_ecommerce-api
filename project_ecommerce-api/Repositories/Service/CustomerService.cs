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
        private readonly ICart cartService;
        private readonly IProduct productService;

        public CustomerService(ApplicationDbContext context, ICart cartService,
            IProduct productService)
        {
            this.context = context;
            this.cartService = cartService;
            this.productService = productService;
        }

        public async Task<ApiResponse<CustomerLoginResponse>> LoginAsync(CustomerLoginDto customerLoginDto)
        {
            try
            {
                // Find customer
                var customer = await context.Customers
                    .FirstOrDefaultAsync(c => c.Email.Equals(customerLoginDto.Email));
                if (customer != null)
                {
                    // Verify a password during login
                    var passwordHasher = new PasswordHasher<CustomerLoginDto>();
                    var passwordVerificationResult = passwordHasher.VerifyHashedPassword(customerLoginDto, customer.PasswordHash, customerLoginDto.Password);
                    // Password is valid
                    if (passwordVerificationResult == PasswordVerificationResult.Success)
                    {
                        // Get cart of customer
                        Cart? cart = await context.Carts
                            .Where(c => c.CustomerId == customer.Id)
                            .FirstOrDefaultAsync();
                        if (cart == null)
                        {
                            // Create cart
                            cart = new Cart()
                            {
                                Id = Guid.NewGuid(),
                                CustomerId = customer.Id,
                            };
                            await cartService.CreateCart(cart);
                        }

                        // Convert to customer dto
                        var customerDto = new CustomerLoginResponse()
                        {
                            CartId = cart.Id,
                            FirstName = customer.FirstName,
                            LastName = customer.LastName,
                            Email = customer.Email
                        };
                        // Login success
                        return new ApiResponse<CustomerLoginResponse>(customerDto, "Đăng nhập thành công");
                    }
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

                // Create cart when create customer success
                Cart cart = new Cart()
                {
                    Id = Guid.NewGuid(),
                    CustomerId = customer.Id
                };
                await cartService.CreateCart(cart);

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

        public async Task<ApiResponse<bool>> AddCustomerAddress(CustomerAddressDto addressDto, string email)
        {
            try
            {
                // Get customer
                var customer = await GetCustomerByEmail(email);
                if (customer == null)
                {
                    return new ApiResponse<bool>(new List<string>()
                    {
                        "Customer not found"
                    });
                }
                // Create new address customer
                CustomerAddress address = new CustomerAddress()
                {
                    Id = Guid.NewGuid(),
                    CustomerName = addressDto.CustomerName,
                    City = addressDto.City,
                    District = addressDto.District,
                    Address = addressDto.Address,
                    PhoneNumber = addressDto.PhoneNumber,
                    CustomerId = customer.Id
                };
                // Check if customer address is first, then asign that address is default
                CustomerAddress? customerAddress = await context.CustomerAddresses
                    .FirstOrDefaultAsync(c => c.CustomerId.Equals(customer.Id));
                if (customerAddress == null) {
                    address.IsDefault = true;
                } else
                {
                    address.IsDefault = false;
                }
                await context.CustomerAddresses.AddAsync(address);
                await context.SaveChangesAsync();
                return new ApiResponse<bool>(true, "Add customer address successfully");
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(new List<string>()
                {
                    ex.Message
                });
            }
        }

        public async Task<ApiResponse<bool>> UpdateCustomerAddress(CustomerAddressDto addressDto, string email)
        {
            try
            {
                // Find customer
                CustomerDto? customer = await GetCustomerByEmail(email);
                if (customer == null)
                {
                    return new ApiResponse<bool>(new List<string>()
                    {
                        "Customer not found"
                    });
                }
                // Create new object customer address
                var address = new CustomerAddress()
                {
                    Id = addressDto.Id,
                    CustomerName = addressDto.CustomerName,
                    City = addressDto.City,
                    District = addressDto.District,
                    Address = addressDto.Address,
                    PhoneNumber = addressDto.PhoneNumber,
                    IsDefault = addressDto.IsDefault,
                    CustomerId = customer.Id
                };
                // Handle set defaul address is false
                if (addressDto.IsDefault)
                {
                    var customerAddresses = await GetCustomerAddresses(customer.Id);
                    if (customerAddresses.Count() > 1)
                    {
                        foreach (var customerAddress in customerAddresses)
                        {
                            if (!customerAddress.Id.Equals(address.Id))
                            {
                                var addressItem = new CustomerAddress()
                                {
                                    Id = customerAddress.Id,
                                    CustomerName = customerAddress.CustomerName,
                                    City = customerAddress.City,
                                    District = customerAddress.District,
                                    Address = customerAddress.Address,
                                    PhoneNumber = customerAddress.PhoneNumber,
                                    CustomerId = customer.Id,
                                    IsDefault = false
                                };
                                // Update in database
                                context.CustomerAddresses.Update(addressItem);
                                await context.SaveChangesAsync();
                            }
                        }
                    }
                }
                context.CustomerAddresses.Update(address);
                await context.SaveChangesAsync();
                return new ApiResponse<bool>(true, "Update customer address successfully");
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(new List<string>()
                {
                    ex.Message
                });
            }
        }

        public async Task<ApiResponse<bool>> DeleteCustomerAddress(Guid customerAddressId)
        {
            try
            {
                // Find customer address
                var customerAddress = await context.CustomerAddresses.FindAsync(customerAddressId);
                if (customerAddress == null)
                {
                    return new ApiResponse<bool>(new List<string>()
                    {
                        "Customer address not found"
                    });
                }
                context.CustomerAddresses.Remove(customerAddress);
                await context.SaveChangesAsync();

                // Get all customer address
                var customerAddresses = await GetCustomerAddresses(customerAddress.CustomerId);
                if (customerAddresses.Count > 0)
                {
                    foreach (var address in customerAddresses)
                    {
                        CustomerAddress item = new CustomerAddress()
                        {
                            Id = address.Id,
                            CustomerName = address.CustomerName,
                            City = address.City,
                            District = address.District,
                            Address = address.Address,
                            CustomerId = customerAddress.CustomerId,
                            PhoneNumber = address.PhoneNumber,
                            IsDefault = true
                        };
                        context.CustomerAddresses.Update(item);
                        await context.SaveChangesAsync();
                        break;
                    }
                }
                return new ApiResponse<bool>(true, "Delete customer address successfully");
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(new List<string>()
                {
                    ex.Message
                });
            }
        }

        public async Task<CustomerDto> GetCustomerByEmail(string email)
        {
            try
            {
                // Find customer by email
                var customer = await context.Customers
                    .Where(c => c.Email.Trim().ToLower().Equals(email.Trim().ToLower()))
                    .Select(c => new CustomerDto()
                    {
                        Id = c.Id,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Email = c.Email,
                        Picture = c.Picture,
                        PhoneNumber = c.PhoneNumber,
                        IsActive = c.IsActive,
                        CreatedDate = c.CreatedDate
                    }).FirstOrDefaultAsync();
                if (customer == null)
                {
                    throw new Exception("Customer not found");
                }
                return customer;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<CustomerDto>> GetInfo(string email)
        {
            try
            {
                // Get customer by email
                CustomerDto customer = await GetCustomerByEmail(email);
                // Get customer address
                customer.CustomerAddresses = await GetCustomerAddresses(customer.Id);
                // Get orders
                customer.Order = await GetCustomerOrders(customer.Id);
                return new ApiResponse<CustomerDto>(customer);
            }
            catch (Exception ex)
            {
                return new ApiResponse<CustomerDto>(new List<string>()
                {
                    ex.Message
                });
            }
        }

        public async Task<List<CustomerAddressDto>> GetCustomerAddresses(Guid customerId)
        {
            try
            {
                // Get customer address
                var customerAddress = await context.CustomerAddresses
                    .Where(cd => cd.CustomerId.Equals(customerId))
                    .Select(cd => new CustomerAddressDto()
                    {
                        Id = cd.Id,
                        CustomerName = cd.CustomerName,
                        City = cd.City,
                        District = cd.District,
                        Address = cd.Address,
                        IsDefault = cd.IsDefault,
                        PhoneNumber = cd.PhoneNumber
                    }).ToListAsync();
                return customerAddress;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OrderDto?> GetCustomerOrders(Guid customerId)
        {
            try
            {
                // Get order
                var orderDto = await context.Orders
                    .Join(
                        context.Statuses,
                        order => order.StatusId,
                        status => status.Id,
                        (order, status) => new OrderDto()
                        {
                            Id = order.Id,
                            PhoneNumber = order.PhoneNumber,
                            Address = order.Address,
                            Note = order.Note,
                            OrderDate = order.OrderDate,
                            StatusType = status.Type
                        }
                    ).FirstOrDefaultAsync();

                if (orderDto == null )
                {
                    return null;
                }

                // Get order items
                var orderItems = await context.OrderItems
                    .Where(oi => oi.OrderId.Equals(orderDto.Id))
                    .Select(oi => new OrderItemDto()
                    {
                        ProductId = oi.ProductId,
                        Price = oi.Price,
                        Quantity = oi.Quantity,
                    })
                    .ToListAsync();
                // Get product
                foreach (var item in orderItems)
                {
                    var product = await productService.GetProductByIdAsync(item.ProductId);
                    if (product != null)
                    {
                        item.Name = product.Name;
                        item.DefaultImage = product.DefaultImage;
                        item.TextUrl = product.TextUrl;
                    }
                }

                orderDto.OrderItems = orderItems;
                return orderDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
