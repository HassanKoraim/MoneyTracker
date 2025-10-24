using System.ComponentModel.DataAnnotations;

namespace MoneyTracker_API.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }  // e.g., "Cash", "Credit Card", "Bank Transfer"

        [Required]
        public string? Description { get; set; } = null!;

        // Navigation properties
        public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();
        public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }

}