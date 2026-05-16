using static MoneyTracker.Domain.Enums.SD;

namespace MoneyTracker.Application.DTOs.CategoryDTOs
{
    public class CategoryDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public CategoryType? Type { get; set; }
        public int? ParentCategoryId { get; set; }
        public string? ParentCategoryName { get; set; }
        public List<CategoryDto> SubCategories { get; set; } = new List<CategoryDto>();
    }
}
