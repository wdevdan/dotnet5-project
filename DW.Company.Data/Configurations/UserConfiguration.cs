using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DW.Company.Entities.Entity;

namespace DW.Company.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).HasColumnName("id").IsRequired();
            builder.Property(p => p.FirstName).HasColumnName("firstname").HasMaxLength(60).IsRequired();
            builder.Property(p => p.LastName).HasColumnName("lastname").HasMaxLength(60).IsRequired();
            builder.Property(p => p.CustomerId).HasColumnName("customer_id");
            builder.Property(p => p.Login).HasColumnName("login").HasMaxLength(60).IsRequired();
            builder.Property(p => p.Email).HasColumnName("email").HasMaxLength(60).IsRequired();
            builder.Property(p => p.Role).HasColumnName("role").IsRequired().HasDefaultValue("customer");
            builder.Property(p => p.Password).HasColumnName("password").HasMaxLength(254).IsRequired();
            builder.Property(p => p.ValidSince).HasColumnName("valid_since").IsRequired();
            builder.Property(p => p.ValidUntil).HasColumnName("valid_until").IsRequired();

            builder.HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(k => k.CustomerId);
        }
    }
}
