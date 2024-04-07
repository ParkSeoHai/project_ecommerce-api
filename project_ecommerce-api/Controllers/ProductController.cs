using Microsoft.AspNetCore.Mvc;
using project_ecommerce_api.Repositories.Interface;

namespace project_ecommerce_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct productService;

        public ProductController(IProduct productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpGet]
        [Route("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await productService.GetProductsAsync();
                return Ok(products);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetProductsByCategory")]
        public async Task<IActionResult> GetProductsByCategory(string name, int limit)
        {
            try
            {
                var products = await productService.GetProductsByCategoryAsync(name, limit);
                return Ok(products);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetFlashSale")]
        public async Task<IActionResult> GetFlashSale()
        {
            try
            {
                var flashSale = await productService.GetFlashSaleAsync();
                return Ok(flashSale);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetProductByName")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            try
            {
                var product = await productService.GetProductByNameAsync(name);
                return Ok(product);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
