using Microsoft.EntityFrameworkCore;
using project_ecommerce_api.Data;
using project_ecommerce_api.DTOs;
using project_ecommerce_api.Repositories.Interface;

namespace project_ecommerce_api.Repositories.Service
{
    public class CategoryService : ICategory
    {
        private readonly ApplicationDbContext context;

        public CategoryService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<CategoryDto>> GetCategoriesByLevelAsync(int level)
        {
            List<CategoryDto> categoriesDtos = new List<CategoryDto>();
            // Query database
            var categories = await context.Categories.Where(c => c.Level == level).ToListAsync();

            if(categories.Count() <= 0)
            {
                throw new NotImplementedException("No data");
            }

            foreach (var category in categories)
            {
                CategoryDto categoryDto = new CategoryDto()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Level = category.Level,
                    CategoryId = category.CategoryId
                };
                categoriesDtos.Add(categoryDto);
            }
            return categoriesDtos;
        }

        public async Task<List<CategoryDto>> GetCategoriesLevel2Async(string categoryName)
        {
            List<CategoryDto> categoriesDtos = new List<CategoryDto>();
            // Query database
            var categoryFind = await context.Categories
                .Where(c => c.Name.ToUpper() == categoryName.ToUpper())
                .FirstOrDefaultAsync();

            if (categoryFind == null)
            {
                throw new Exception("Category Not Found");
            }

            var categories = await context.Categories
                .Where(c => c.CategoryId == categoryFind.Id.ToString())
                .ToListAsync();

            foreach (var category in categories)
            {
                CategoryDto categoryDto = new CategoryDto()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Level = category.Level,
                    CategoryId = category.CategoryId
                };
                categoriesDtos.Add(categoryDto);
            }
            return categoriesDtos;
        }
    }
}
