using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DW.Company.Entities.Entity;

namespace DW.Company.Data.Configurations
{
    public class ProductFileConfiguration : IEntityTypeConfiguration<ProductFile>
    {
        public void Configure(EntityTypeBuilder<ProductFile> builder)
        {
            builder.ToTable("product_file");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).HasColumnName("id").IsRequired();
            builder.Property(p => p.FileItemId).HasColumnName("file_item_id");
            builder.Property(p => p.ProductId).HasColumnName("product_id");
            builder.Property(p => p.Order).HasColumnName("order");

            builder.HasOne(o => o.FileItem)
                .WithMany()
                .HasForeignKey(f => f.FileItemId);

            builder.HasOne(o => o.Product)
                .WithMany(m => m.Files)
                .HasForeignKey(f => f.ProductId);
        }
    }
}