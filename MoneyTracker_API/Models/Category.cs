using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static MoneyTracker_Utility.SD;

namespace MoneyTracker_API.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public int SubcategoryId { get; set; }

        [Required]
        public CategoryType Type { get; set; }  // Income or Expense

        // Self-referencing foreign key for subcategories
        public int? ParentCategoryId { get; set; }

        [ForeignKey("ParentCategoryId")]
        public virtual Category? ParentCategory { get; set; }

        // Navigation properties
        public virtual ICollection<Category> SubCategories { get; set; } = new List<Category>();
        // Navigation properties
        public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();
        public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}
