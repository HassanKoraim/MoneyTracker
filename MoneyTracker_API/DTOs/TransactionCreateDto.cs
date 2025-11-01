using static MoneyTracker_Utility.SD;

namespace MoneyTracker_API.DTOs
{
    public class TransactionCreateDto
    {
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public TransactionType TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public int CategoryId { get; set; }
        public int PaymentMethodId { get; set; }
        public bool IsRecurring { get; set; }
        public RecurrenceType? RecurrenceType { get; set; }
        public DateTime? RecurrenceEndDate { get; set; }
        public string? ImageUrl { get; set; }
    }
}
