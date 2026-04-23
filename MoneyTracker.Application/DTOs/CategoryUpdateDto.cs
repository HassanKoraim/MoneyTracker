using static MoneyTracker.Domain.Enums.SD;

namespace MoneyTracker.Application.DTOs
{
    public class CategoryUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public CategoryType Type { get; set; }
        public string? Description { get; set; }
        public int? ParentCategoryId { get; set; }
        public List<CategoryUpdateDto> SubCategories { get; set; } = new List<CategoryUpdateDto>();
    }
}