using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DW.Company.Entities.Entity;

namespace DW.Company.Data.Configurations
{
    public class CustomerProductConfiguration : IEntityTypeConfiguration<CustomerProduct>
    {
        public void Configure(EntityTypeBuilder<CustomerProduct> builder)
        {
            builder.ToTable("customer_product");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).HasColumnName("id").IsRequired();
            builder.Property(p => p.CustomerId).HasColumnName("customer_id").IsRequired();
            builder.Property(p => p.ProductId).HasColumnName("product_id").IsRequired();

            builder.HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(f => f.CustomerId);

            builder.HasOne(o => o.Product)
                .WithMany()
                .HasForeignKey(f => f.ProductId);
        }
    }
}