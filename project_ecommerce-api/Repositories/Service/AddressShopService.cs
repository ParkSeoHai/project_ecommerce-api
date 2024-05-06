using Microsoft.EntityFrameworkCore;
using project_ecommerce_api.DTOs;
using project_ecommerce_api.Models;
using project_ecommerce_api.Repositories.Interface;

namespace project_ecommerce_api.Repositories.Service
{
    public class AddressShopService : IAddressShop
    {
        private readonly ApplicationDbContext context;

        public AddressShopService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<AddressShopDto>> GetAddressShopsAsync()
        {
            var addressShopDtos = new List<AddressShopDto>();
            // Query database
            var addressShops = await context.AddressShops.ToListAsync();
            foreach (var addressShop in addressShops)
            {
                var addressShopDto = new AddressShopDto()
                {
                    Id = addressShop.Id,
                    NameShop = addressShop.NameShop,
                    City = addressShop.City,
                    District = addressShop.District,
                    Address = addressShop.Address,
                    PhoneNumber = addressShop.PhoneNumber,
                    UrlMap = addressShop.UrlMap,
                    Note = addressShop.Note
                };
                addressShopDtos.Add(addressShopDto);
            }
            return addressShopDtos;
        }
    }
}
