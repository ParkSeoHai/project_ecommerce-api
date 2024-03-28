using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project_ecommerce_api.Repositories.Interface;

namespace project_ecommerce_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressShopController : ControllerBase
    {
        private readonly IAddressShop addressShopService;

        public AddressShopController(IAddressShop addressShopService)
        {
            this.addressShopService = addressShopService;
        }

        [HttpGet]
        [Route("GetAddressShops")]
        public async Task<IActionResult> GetAddressShops() {
            var addressShops = await addressShopService.GetAddressShopsAsync();
            return Ok(addressShops);
        }
    }
}
