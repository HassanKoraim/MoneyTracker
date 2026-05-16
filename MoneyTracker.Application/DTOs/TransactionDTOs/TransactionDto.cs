using static MoneyTracker.Domain.Enums.SD;

namespace MoneyTracker.Application.DTOs.TransactionDTOs
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? ParentCategoryName { get; set; } 
        public int PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; } = string.Empty;
        public bool IsRecurring { get; set; }
        public string? RecurrenceType { get; set; }
        public DateTime? RecurrenceEndDate { get; set; }
        public string? ImageUrl { get; set; }
    }
}
