using Microsoft.EntityFrameworkCore;
using project_ecommerce_api.DTOs;
using project_ecommerce_api.Models;
using project_ecommerce_api.Repositories.Interface;

namespace project_ecommerce_api.Repositories.Service
{
    public class CartService : ICart
    {
        private readonly ApplicationDbContext context;
        private readonly IProduct productService;

        public CartService(ApplicationDbContext context, IProduct productService)
        {
            this.context = context;
            this.productService = productService;
        }

        public async Task<bool> CreateCart(Cart cart)
        {
            try
            {
                await context.Carts.AddAsync(cart);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<CartDto>> GetCartByIdAsync(Guid id)
        {
            try
            {
                // Find cart by cart id
                var cart = await context.Carts.FindAsync(id);
                if (cart != null)
                {
                    // Get items cart
                    var cartItems = await context.CartItems
                        .Where(ci => ci.CartId == cart.Id)
                        .Select(ci => new CartItemDto()
                        {
                            Name = ci.Product.Name,
                            DefaultImage = ci.Product.DefaultImage,
                            Price = ci.Product.Price - (ci.Product.Price * ci.Product.Discount / 100),
                            Option = ci.Option,
                            Quantity = ci.Quantity,
                            TextUrl = ci.Product.TextUrl,
                            QuantityMax = ci.Product.Quantity
                        }).ToListAsync();
                    // Convert to cart dto
                    CartDto cartDto = new CartDto()
                    {
                        CartId = cart.Id,
                        CartItems = cartItems
                    };
                    // Return data
                    return new ApiResponse<CartDto>(cartDto, "Lấy thông tin giỏ hàng thành công");
                }

                // If cart is null
                return new ApiResponse<CartDto>(new List<string>
                {
                    "Không tìm thấy thông tin giỏ hàng. Vui lòng báo lỗi"
                });
            }
            catch (Exception ex)
            {
                return new ApiResponse<CartDto>(new List<string>
                {
                    ex.Message
                });
            }
        }

        public async Task<ApiResponse<bool>> AddItemCartAsync(CartPostDto cartItemPost)
        {
            try
            {
                // Find product by text url
                var product = await productService.GetProductByTextUrlAsync(cartItemPost.ProductUrl);
                if (product == null)
                {
                    return new ApiResponse<bool>(new List<string>
                    {
                        "Product not found"
                    });
                }
                CartItem cartItem = new CartItem()
                {
                    CartId = cartItemPost.CartId,
                    ProductId = product.Id,
                    Option = cartItemPost.Option,
                    Quantity = cartItemPost.Quantity
                };
                await context.CartItems.AddAsync(cartItem);
                await context.SaveChangesAsync();
                return new ApiResponse<bool>(true);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(new List<string>
                {
                    ex.Message
                });
            }
        }

        public async Task<ApiResponse<bool>> RemoveItemCartAsync(Guid cartId, string productUrl)
        {
            try
            {
                // Find product by product text url
                var product = await productService.GetProductByTextUrlAsync(productUrl);
                if (product == null)
                {
                    return new ApiResponse<bool>(new List<string>()
                    {
                        "Product not found"
                    });
                }

                var cartItem = new CartItem()
                {
                    CartId = cartId,
                    ProductId = product.Id
                };

                context.CartItems.Remove(cartItem);
                await context.SaveChangesAsync();
                return new ApiResponse<bool>(true);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(new List<string>()
                {
                    ex.Message
                });
            }
        }

        public async Task<ApiResponse<bool>> UpdateItemCartAsync(CartPostDto cartItemPost)
        {
            try
            {
                // Find product by text url get product id
                var product = await productService.GetProductByTextUrlAsync(cartItemPost.ProductUrl);
                if (product == null)
                {
                    return new ApiResponse<bool>(new List<string>
                    {
                        "Product not found"
                    });
                }
                var cartItem = new CartItem()
                {
                    CartId = cartItemPost.CartId,
                    ProductId = product.Id,
                    Quantity = cartItemPost.Quantity,
                    Option = cartItemPost.Option
                };
                context.CartItems.Update(cartItem);
                await context.SaveChangesAsync();
                return new ApiResponse<bool>(true);
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
