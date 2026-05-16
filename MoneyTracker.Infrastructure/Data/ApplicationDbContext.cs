using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Domain.Entities;
using MoneyTracker.Domain.Entities.Identity;
using MoneyTracker.Infrastructure.Data.Configurations;

namespace MoneyTracker.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

       /* public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }*/
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration).Assembly);
            // complete the rest of the configurations
        }
    }
}
