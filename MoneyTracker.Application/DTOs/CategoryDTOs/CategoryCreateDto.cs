using System.ComponentModel.DataAnnotations;
using static MoneyTracker.Domain.Enums.SD;

namespace MoneyTracker.Application.DTOs.CategoryDTOs
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public CategoryType? Type { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}