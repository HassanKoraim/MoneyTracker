using static MoneyTracker_Utility.SD;

namespace MoneyTracker_API.DTOs
{
    /// <summary>
    /// This DTO would be useful for reporting/analytics scenarios where you want to show:
        // Category information
        //Total amount of all transactions in that category
        //Count of transactions in that category
    /// </summary>
    public class CategoryWithTransactionsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public CategoryType Type { get; set; }
        public string? Description { get; set; }
        public int? ParentCategoryId { get; set; }
        public string? ParentCategoryName { get; set; }
        public List<CategoryDto> SubCategories { get; set; } = new List<CategoryDto>();
        public decimal TotalAmount { get; set; }
        public int TransactionCount { get; set; }
    }
}
