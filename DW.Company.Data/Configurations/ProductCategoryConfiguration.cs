using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DW.Company.Entities.Entity;

namespace DW.Company.Data.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("product_category");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).HasColumnName("id").IsRequired();
            builder.Property(p => p.CategoryId).HasColumnName("category_id").IsRequired();
            builder.Property(p => p.ProductId).HasColumnName("product_id").IsRequired();

            builder.HasOne(o => o.Category)
                .WithMany()
                .HasForeignKey(f => f.CategoryId);

            builder.HasOne(o => o.Product)
                .WithMany(m => m.Categories)
                .HasForeignKey(f => f.ProductId);
        }
    }
}