using MoneyTracker_Utility;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static MoneyTracker_Utility.SD;

namespace MoneyTracker_API.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public TransactionType transactionType {  get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime TransactionDate { get; set; } = DateTime.Now;

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; } = null!;

        [Required]
        public int PaymentMethodId { get; set; }

        [ForeignKey("PaymentMethodId")]
        public virtual PaymentMethod PaymentMethod { get; set; } = null!;

        public bool IsRecurring { get; set; } = false;

        // Recurrence properties
        public RecurrenceType? RecurrenceType { get; set; } // Daily, Weekly, Monthly, etc.
        public DateTime? RecurrenceEndDate { get; set; }

        [StringLength(500)]
        public string? ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
