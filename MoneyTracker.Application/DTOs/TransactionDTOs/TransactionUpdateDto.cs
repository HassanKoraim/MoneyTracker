using static MoneyTracker.Domain.Enums.SD;

namespace MoneyTracker.Application.DTOs.TransactionDTOs
{
    public class TransactionUpdateDto
    {
        public decimal Amount { get; set; }
        public string? Description { get; set; } 
        public DateTime TransactionDate { get; set; }
        public int CategoryId { get; set; }
        public int PaymentMethodId { get; set; }
        public bool? IsRecurring { get; set; }
        public RecurrenceType? RecurrenceType { get; set; }
        public DateTime? RecurrenceEndDate { get; set; }
        public string? ImageUrl { get; set; }
    }
}
