using MoneyTracker.Application.DTOs;
using MoneyTracker.Domain.Entities;
using static MoneyTracker.Domain.Enums.SD;

namespace MoneyTracker.Application.RepositoryContracts
{
    public interface ICategoriesRepository : IRepositoryContracts<Category>
    {
      Task<List<Category>> GetParentCategories(); 
      Task<List<Category>> GetSubCategoriesByParentId(int parentCategoryId);
      Task<List<Category>> GetParentCategoriesByType(CategoryType? type);
      Task<bool> CategoryExists(string categoryName, CategoryType categoryType);
        // Update operations  
      Task<Category> Update(Category category);
    }
}
