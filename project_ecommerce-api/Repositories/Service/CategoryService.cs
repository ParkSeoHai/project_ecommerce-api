using Microsoft.EntityFrameworkCore;
using project_ecommerce_api.DTOs;
using project_ecommerce_api.Models;
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
            try
            {
                List<CategoryDto> categoriesDtos = new List<CategoryDto>();
                // Query database
                var categories = await context.Categories
                    .Where(c => c.Level == level && c.Publish == true)
                    .OrderBy(c => c.Order)
                    .ToListAsync();

                foreach (var category in categories)
                {
                    CategoryDto categoryDto = new CategoryDto()
                    {
                        Id = category.Id,
                        Name = category.Name.Trim(),
                        Level = category.Level,
                        Icon = category.Icon,
                        CategoryId = category.CategoryId,
                        Order = category.Order,
                        TextUrl = category.TextUrl.Trim(),
                    };
                    categoriesDtos.Add(categoryDto);
                }
                return categoriesDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<CategoryDto>> GetCategoriesLevel2Async(string categoryName)
        {
            try
            {
                // Query database
                var categoryFind = await context.Categories
                    .Where(c => c.Name.ToUpper() == categoryName.ToUpper() && c.Publish == true)
                    .FirstOrDefaultAsync();

                if (categoryFind == null)
                {
                    throw new Exception("Category Not Found");
                }

                List<CategoryDto> categoriesDtos = new List<CategoryDto>();

                var categories = await context.Categories
                    .Where(c => c.CategoryId == categoryFind.Id.ToString())
                    .ToListAsync();

                foreach (var category in categories)
                {
                    CategoryDto categoryDto = new CategoryDto()
                    {
                        Id = category.Id,
                        Name = category.Name.Trim(),
                        Level = category.Level,
                        Icon = category.Icon,
                        CategoryId = category.CategoryId,
                        Order = category.Order,
                        TextUrl = category.TextUrl.Trim(),
                    };
                    categoriesDtos.Add(categoryDto);
                }
                return categoriesDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(Guid id)
        {
            try
            {
                // Find category
                var category = await context.Categories.FindAsync(id);
                if(category == null)
                {
                    throw new Exception("Category not found");
                }
                // Convert to category dto
                var categoryDto = new CategoryDto()
                {
                    Id = category.Id,
                    Name= category.Name.Trim(),
                    Level= category.Level,
                    Icon = category.Icon,
                    CategoryId = category.CategoryId,
                    Order = category.Order,
                    TextUrl = category.TextUrl.Trim(),
                };
                return categoryDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<CategoryDto>> GetCategoriesByIdAsync(Guid id)
        {
            var categories = new List<CategoryDto>();
            var categoryId = id;
            while (categoryId != Guid.Empty)
            {
                // Get category
                CategoryDto? categoryParent = await GetCategoryByIdAsync(categoryId);
                if (categoryParent == null) break;
                // Add to list
                categories.Insert(0, categoryParent);
                // Break loop
                if (categoryParent.CategoryId == null)
                {
                    categoryId = Guid.Empty;
                }
                else
                {
                    categoryId = Guid.Parse(categoryParent.CategoryId);
                }
            }
            return categories;
        }

        public async Task<CategoryDto> GetCategoryByTextUrlAsync(string textUrl)
        {
            try
            {
                // Find category
                CategoryDto categoryDto = await context.Categories
                    .Where(c => c.TextUrl.Trim().ToLower() == textUrl.Trim().ToLower())
                    .Select(c => new CategoryDto()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        CategoryId = c.CategoryId,
                        Icon = c.Icon,
                        Level = c.Level,
                        Order = c.Order,
                        Publish = c.Publish,
                        TextUrl = c.TextUrl
                    }).SingleAsync();

                if (categoryDto == null)
                {
                    throw new Exception("Category not found");
                }
                return categoryDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}