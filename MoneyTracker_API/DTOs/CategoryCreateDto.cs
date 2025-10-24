using System.ComponentModel.DataAnnotations;
using static MoneyTracker_Utility.SD;

namespace MoneyTracker_API.DTOs
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 50 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category type is required")]
        public CategoryType Type { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string? Description { get; set; }

        public int? ParentCategoryId { get; set; }
    }
}