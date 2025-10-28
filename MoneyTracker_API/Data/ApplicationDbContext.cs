using Microsoft.EntityFrameworkCore;
using MoneyTracker_API.Models;
using static MoneyTracker_Utility.SD;

namespace MoneyTracker_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

       /* public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }*/
       public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure self-referencing relationship for Categories
            modelBuilder.Entity<Category>()
                .HasMany(c => c.SubCategories)
                .WithOne(c => c.ParentCategory)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);            

            // Seed data with categories and subcategories
            modelBuilder.Entity<Category>().HasData(
                // Income Categories
                new Category { Id = 1, Name = "Salary", Type = CategoryType.Income, Description = "Employment income" },
                new Category { Id = 2, Name = "Freelance", Type = CategoryType.Income, Description = "Freelance work" },
                new Category { Id = 3, Name = "Investment", Type = CategoryType.Income, Description = "Investment returns" },

                // Income Subcategories
                new Category { Id = 4, Name = "Stocks", Type = CategoryType.Income, ParentCategoryId = 3, Description = "Stock market investments" },
                new Category { Id = 5, Name = "Dividends", Type = CategoryType.Income, ParentCategoryId = 3, Description = "Dividend income" },

                // Expense Categories
                new Category { Id = 6, Name = "Food", Type = CategoryType.Expense, Description = "Food and dining" },
                new Category { Id = 7, Name = "Transportation", Type = CategoryType.Expense, Description = "Transport costs" },
                new Category { Id = 8, Name = "Utilities", Type = CategoryType.Expense, Description = "Bills and utilities" },
                new Category { Id = 9, Name = "Entertainment", Type = CategoryType.Expense, Description = "Entertainment expenses" },

                // Expense Subcategories
                new Category { Id = 10, Name = "Groceries", Type = CategoryType.Expense, ParentCategoryId = 6, Description = "Grocery shopping" },
                new Category { Id = 11, Name = "Restaurants", Type = CategoryType.Expense, ParentCategoryId = 6, Description = "Dining out" },
                new Category { Id = 12, Name = "Fuel", Type = CategoryType.Expense, ParentCategoryId = 7, Description = "Vehicle fuel" },
                new Category { Id = 13, Name = "Public Transport", Type = CategoryType.Expense, ParentCategoryId = 7, Description = "Bus, train, etc." }
            );

            modelBuilder.Entity<PaymentMethod>().HasData(
                new PaymentMethod { Id = 1, Name = "Cash", Description = "Physical cash" },
                new PaymentMethod { Id = 2, Name = "Credit Card", Description = "Credit card payments" },
                new PaymentMethod { Id = 3, Name = "Bank Transfer", Description = "Direct bank transfer" },
                new PaymentMethod { Id = 4, Name = "Digital Wallet", Description = "E-wallet payments" }
            );
        }
    }
}
