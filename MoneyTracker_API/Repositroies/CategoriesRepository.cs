using Microsoft.EntityFrameworkCore;
using MoneyTracker_API.Data;
using MoneyTracker_API.Models;
using MoneyTracker_API.RepositoryContracts;
using MoneyTracker_Utility;

namespace MoneyTracker_API.Repositroies
{
    public class CategoriesRepository : Repository<Category>, ICategoriesRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoriesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetParentCategories()
        {
            List<Category> categories = await _context.Categories
                .Where(c => c.ParentCategoryId == null).ToListAsync();
            return categories;
        }

        public async Task<List<Category>> GetParentCategoriesByType(SD.CategoryType type)
        {
            List<Category> categories = await _context.Categories
               .Where(c => c.Type == type && c.ParentCategoryId == null).ToListAsync();
            return categories;
        }

        public async Task<List<Category>> GetSubCategoriesByParentId(int parentCategoryId)
        {
            List<Category> categories = await _context.Categories
               .Where(c => c.ParentCategoryId == parentCategoryId).ToListAsync();
            return categories;
        }
    }
}
