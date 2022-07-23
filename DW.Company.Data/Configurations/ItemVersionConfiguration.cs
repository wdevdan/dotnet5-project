using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DW.Company.Entities.Entity;

namespace DW.Company.Data.Configurations
{
    public class ItemVersionConfiguration : IEntityTypeConfiguration<ItemVersion>
    {
        public void Configure(EntityTypeBuilder<ItemVersion> builder)
        {
            builder.ToTable("version");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).HasColumnName("id").IsRequired();
            builder.Property(p => p.Code).HasColumnName("code").HasMaxLength(50).IsRequired();
            builder.Property(p => p.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
        }
    }
}