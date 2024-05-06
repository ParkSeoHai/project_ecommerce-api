using Microsoft.EntityFrameworkCore;
using project_ecommerce_api.DTOs;
using project_ecommerce_api.Models;
using project_ecommerce_api.Repositories.Interface;

namespace project_ecommerce_api.Repositories.Service
{
    public class ProductShopService : IProductShop
    {
        private readonly ApplicationDbContext context;

        public ProductShopService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<ProductShopDto>> GetProductShopByOptionAsync(Guid optionId)
        {
            try
            {
                // Query find option
                var option = await context.Options.FindAsync(optionId);
                if(option == null)
                {
                    throw new Exception("Option not found");
                }
                // Query product shop
                var productShops = await context.ProductShops
                    .Where(ps => ps.OptionId == option.Id)
                    .Select(ps => new
                    {
                        AddressShop = ps.AddressShop,
                        Quantity = ps.Quantity
                    })
                    .ToListAsync();

                var productShopDtos = new List<ProductShopDto>();

                foreach (var productShop in productShops)
                {
                    var productShopDto = new ProductShopDto()
                    {
                        Id = productShop.AddressShop.Id,
                        NameShop = productShop.AddressShop.NameShop,
                        City = productShop.AddressShop.City,
                        District = productShop.AddressShop.District,
                        Address = productShop.AddressShop.Address,
                        Note = productShop.AddressShop.Note,
                        PhoneNumber = productShop.AddressShop.PhoneNumber,
                        UrlMap = productShop.AddressShop.UrlMap,
                        Quantity = productShop.Quantity
                    };
                    productShopDtos.Add(productShopDto);
                }
                return productShopDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
