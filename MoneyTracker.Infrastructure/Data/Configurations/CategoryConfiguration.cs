using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyTracker.Domain.Entities;


namespace MoneyTracker.Infrastructure.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
           builder.HasKey(c => c.Id);
           builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(50);
           builder.Property(c => c.SubcategoryId).IsRequired();
           builder.Property(c => c.Type).IsRequired();
           builder.HasMany(c => c.SubCategories)
                   .WithOne(c => c.ParentCategory)
                   .HasForeignKey(c => c.ParentCategoryId)
                   .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
