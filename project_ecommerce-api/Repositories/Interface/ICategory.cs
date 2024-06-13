using project_ecommerce_api.DTOs;

namespace project_ecommerce_api.Repositories.Interface
{
    public interface ICategory
    {
        Task<List<CategoryDto>> GetCategoriesByLevelAsync(int level);
        Task<List<CategoryDto>> GetCategoriesLevel2Async(string categoryName);
        Task<CategoryDto> GetCategoryByIdAsync(Guid id);
        Task<List<CategoryDto>> GetCategoriesByIdAsync(Guid categoryId);
        Task<CategoryDto> GetCategoryByTextUrlAsync(string textUrl);
    }
}
