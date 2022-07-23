using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DW.Company.Entities.Entity;
using DW.Company.Entities.Enums;

namespace DW.Company.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("customer");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).HasColumnName("id").IsRequired();
            builder.Property(p => p.Code).HasColumnName("code").HasMaxLength(50).IsRequired();
            builder.Property(p => p.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
            builder.Property(p => p.Alias).HasColumnName("alias").HasMaxLength(50).IsRequired();
            builder.Property(p => p.Document).HasColumnName("document").HasMaxLength(15).IsRequired();
            builder.Property(p => p.ProductLinkType).HasColumnName("product_link_type").IsRequired().HasDefaultValue((int)ProductLinkType.NONE);

            builder.Property(p => p.CreatedAt).HasColumnName("created").HasMaxLength(50);
            builder.Property(p => p.UpdatedAt).HasColumnName("updated").HasMaxLength(50);
        }
    }
}