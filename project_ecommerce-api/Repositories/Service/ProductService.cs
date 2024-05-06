using Microsoft.EntityFrameworkCore;
using project_ecommerce_api.DTOs;
using project_ecommerce_api.Models;
using project_ecommerce_api.Repositories.Interface;

namespace project_ecommerce_api.Repositories.Service
{
    public class ProductService : IProduct
    {
        private readonly ApplicationDbContext context;
        private readonly IProductColor colorService;
        private readonly IProductOption optionService;
        private readonly IProductShop productShopService;
        private readonly IProperty propertyService;
        private readonly ICategory categoryService;
        private readonly IBrand brandService;
        private readonly IProductImage productImageService;

        public ProductService(ApplicationDbContext context, IProductColor colorService,
            IProductOption optionService, IProductShop productShopService,
            IProperty propertyService, ICategory categoryService,
            IBrand brandService, IProductImage productImageService)
        {
            this.context = context;
            this.colorService = colorService;
            this.optionService = optionService;
            this.productShopService = productShopService;
            this.propertyService = propertyService;
            this.categoryService = categoryService;
            this.brandService = brandService;
            this.productImageService = productImageService;
        }

        public async Task<List<ProductViewDto>> GetProductsAsync()
        {
            List<ProductViewDto> productViewDtos = new List<ProductViewDto>();
            try
            {
                // Query databse
                var products = await context.Products
                    .Where(p => p.Publish)
                    .ToListAsync();

                foreach (var product in products)
                {
                    ProductViewDto productViewDto = new ProductViewDto()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        DefaultImage = product.DefaultImage,
                        Discount = product.Discount,
                        Price = product.Price,
                        Quantity = product.Quantity,
                    };
                    // Get color
                    var colors = await colorService.GetColorsByProductIdAsync(product.Id);
                    productViewDto.ColorCount = colors.Count();
                    // Calc price sale
                    double priceSale = productViewDto.Price - (productViewDto.Price * productViewDto.Discount / 100);
                    productViewDto.PriceSale = priceSale;
                    // Add to list
                    productViewDtos.Add(productViewDto);
                }

                return productViewDtos;
            }
            catch (Exception ex)
            {
                throw new Exception(message: ex.Message);
            }
        }

        public async Task<List<ProductViewDto>> GetProductsByCategoryAsync(string categoryName, int limit)
        {
            List<ProductViewDto> productViewDtos = new List<ProductViewDto>();
            try
            {
                // Get category
                Category? category = await context.Categories.Where(c => c.Name.ToUpper() == categoryName.ToUpper()).FirstOrDefaultAsync();
                if (category == null)
                {
                    throw new Exception(message: "Category not found");
                }

                var products = new List<Product>();

                if (category.Level == 1)
                {
                    // Get data products with limit item
                    products = await context.Products
                        .Where(p => p.Publish && context.Categories
                            .Join(context.Categories, // Joining with the same table (c1 and c2)
                                c1 => c1.Id.ToString(),
                                c2 => c2.CategoryId,
                                (c1, c2) => new { Category1 = c1, Category2 = c2 })
                            .Join(context.Categories, // Joining again with the same table (c2 and c3)
                                cc => cc.Category2.Id.ToString(),
                                c3 => c3.CategoryId,
                                (cc, c3) => new { cc.Category1, cc.Category2, Category3 = c3 })
                            .Where(result => result.Category1.Id == category.Id) // Applying the filtering condition
                            .Select(result => result.Category3.Id) // Selecting the desired properties
                            .Contains(p.CategoryId)) // Checking if the product's CategoryId is in the subquery result
                            .Take(limit)
                        .ToListAsync();
                }
                else if (category.Level == 2)
                {
                    products = await context.Products
                        .Where(p => context.Categories
                            .Join(context.Categories, // Joining with the same table (c1 and c2)
                                c1 => c1.Id.ToString(),
                                c2 => c2.CategoryId,
                                (c1, c2) => new { Category1 = c1, Category2 = c2 })
                            .Where(result => result.Category1.Id == category.Id) // Applying the filtering condition
                            .Select(result => result.Category2.Id) // Selecting the desired properties
                            .Contains(p.CategoryId)) // Checking if the product's CategoryId is in the subquery result
                            .Take(limit)
                        .ToListAsync();
                } else
                {
                    products = await context.Products
                        .Where(p => context.Categories
                            .Where(c => c.Id == category.Id) // Subquery to filter categories based on Id
                            .Select(c => c.Id) // Selecting the Ids from the filtered categories
                            .Contains(p.CategoryId)) // Checking if the product's CategoryId is in the subquery result
                            .Take(limit)
                        .ToListAsync();
                }

                foreach (var product in products)
                {
                    var productDto = new ProductViewDto()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        DefaultImage = product.DefaultImage,
                        Discount = product.Discount,
                        Price = product.Price,
                        Quantity = product.Quantity,
                    };

                    // Get color
                    var colors = await colorService.GetColorsByProductIdAsync(product.Id);
                    productDto.ColorCount = colors.Count();
                    // Calc price sale
                    double priceSale = productDto.Price - (productDto.Price * productDto.Discount / 100);
                    productDto.PriceSale = priceSale;
                    productViewDtos.Add(productDto);
                }

                return productViewDtos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FlashSaleDto> GetFlashSaleAsync()
        {
            // Find flash sale
            var flashSale = await context.FlashSales.Where(f => f.Day != 0).FirstOrDefaultAsync();
            if(flashSale == null)
            {
                throw new Exception("Flash sale not found");
            }

            FlashSaleDto flashSaleDto = new FlashSaleDto()
            {
                Id = flashSale.Id,
                Day = flashSale.Day,
                Hour = flashSale.Hour,
                Minute = flashSale.Minute,
                Second = flashSale.Second
            };

            // Query
            var query = from f in context.FlashSales where f.Id == flashSale.Id
                        join fs in context.FlashSaleItems on f.Id equals fs.FlashSaleId
                        join p in context.Products on fs.ProductId equals p.Id
                        select new { FlashSale = f, FlashSaleItem = fs, Product = p };

            var result = await query.ToListAsync();

            foreach (var item in result)
            {
                // Get flash sale item
                var flashSaleItem = new FlashSaleItemDto()
                {
                    Id = item.Product.Id,
                    Name = item.Product.Name,
                    DefaultImage = item.Product.DefaultImage,
                    Discount = item.FlashSaleItem.Discount,
                    Price = item.Product.Price,
                    Quantity = item.Product.Quantity,
                    QuantitySale = item.FlashSaleItem.QuantitySale,
                    QuantitySold = item.FlashSaleItem.QuantitySold
                };
                // Get color
                var colors = await colorService.GetColorsByProductIdAsync(item.Product.Id);
                flashSaleItem.ColorCount = colors.Count();
                // Calc price sale
                double priceSale = flashSaleItem.Price - (flashSaleItem.Price * flashSaleItem.Discount / 100);
                flashSaleItem.PriceSale = priceSale;
                // Get url
                /*string url = "/Sản phẩm/";
                var categories = await categoryService.GetCategoriesByIdAsync(item.Product.CategoryId);
                foreach (var category in categories)
                {
                    url += category.Name + "/";
                }
                flashSaleItem.Url = url + flashSaleItem.Name;*/
                flashSaleDto.FlashSaleItems.Add(flashSaleItem);
            }

            return flashSaleDto;
        }

        public async Task<ProductDetailDto> GetProductByNameAsync(string name)
        {
            try
            {
                // Find product by name
                Product? product = await context.Products
                    .Where(p => p.Name.ToUpper() == name.ToUpper())
                    .FirstOrDefaultAsync();

                if(product == null)
                {
                    throw new Exception("Product not found");
                }
                // Convert to product dto
                var productDto = new ProductDetailDto()
                {
                    Id = product.Id,
                    Name = product.Name,
                    DefaultImage = product.DefaultImage,
                    Description = product.Description,
                    Discount = product.Discount,
                    Price = product.Price,
                    Quantity = product.Quantity
                };
                // Get brand
                var brand = await brandService.GetBrandByIdAsycn(product.BrandId);
                productDto.Brand = brand;
                // Get categories
                productDto.Categories = await categoryService.GetCategoriesByIdAsync(product.CategoryId);
                // Get image
                var images = await productImageService.GetImagesByProductIdAsync(product.Id);
                productDto.Images = images;
                // Calc price sale
                double priceSale = productDto.Price - (productDto.Price * productDto.Discount / 100);
                productDto.PriceSale = priceSale;
                // Get colors
                productDto.Colors = await colorService.GetColorsByProductIdAsync(productDto.Id);
                // Get options
                foreach (var color in productDto.Colors)
                {
                    var options = await optionService.GetOptionsByColorAsync(color.Id);
                    // Get product shops
                    foreach (var option in options)
                    {
                        var productShops = await productShopService.GetProductShopByOptionAsync(option.Id);
                        option.ProductShops = productShops;
                    }
                    color.Options = options;
                }
                // Get properties
                var properties = await propertyService.GetPropertiesByProductIdAsync(productDto.Id);
                productDto.Properties = properties;
                return productDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
