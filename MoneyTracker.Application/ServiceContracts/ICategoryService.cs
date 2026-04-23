using MoneyTracker.Application.DTOs;
using static MoneyTracker.Domain.Enums.SD;

namespace MoneyTracker.Application.ServiceContracts
{
    public interface ICategoryService
    {
        // Read operations
        Task<CategoryDto?> GetCategory(int id);
        Task<List<CategoryDto>> GetAllCategories();
        Task<List<CategoryDto>> GetParentCategories();
        Task<List<CategoryDto>> GetSubCategoriesByParentId(int parentCategoryId); 

        // Create operations
        Task<CategoryDto> CreateCategory(CategoryCreateDto categoryCreateDto);

        // Update operations  
        Task<CategoryDto?> UpdateCategory(int id, CategoryUpdateDto categoryUpdateDto);
        // Delete operations
        Task<bool> DeleteCategory(int id); // Return bool to indicate success
        Task<List<CategoryDto>> GetParentCategoriesByType(CategoryType? type);

    }
}
