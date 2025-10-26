using MoneyTracker_API.DTOs;
using MoneyTracker_API.Models;
using static MoneyTracker_Utility.SD;

namespace MoneyTracker_API.RepositoryContracts
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
