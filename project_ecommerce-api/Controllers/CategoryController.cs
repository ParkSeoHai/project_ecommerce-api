using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project_ecommerce_api.Repositories.Interface;

namespace project_ecommerce_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory categoryService;

        public CategoryController(ICategory categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        [Route("GetCategoriesByLevel")]
        public async Task<IActionResult> GetCategoriesByLevel(int level)
        {
            var categories = await categoryService.GetCategoriesByLevelAsync(level);
            return Ok(categories);
        }

        [HttpGet]
        [Route("GetCategoriesLevel2")]
        public async Task<IActionResult> GetCategoriesLevel2(string categoryName)
        {
            var categories = await categoryService.GetCategoriesLevel2Async(categoryName);
            return Ok(categories);
        }
    }
}
