using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Infrastructure.Persistence.Configurations;

public class StaffConfiguration : IEntityTypeConfiguration<Staff>
{
    public void Configure(EntityTypeBuilder<Staff> builder)
    {
        builder.ToTable("Staff");
        
        builder.HasKey(s => s.StaffId);
        
        builder.Property(s => s.Username)
               .IsRequired()
               .HasMaxLength(50);
               
        builder.HasIndex(s => s.Username)
               .IsUnique();
               
        builder.Property(s => s.PasswordHash)
               .IsRequired()
               .HasMaxLength(255);
               
        builder.Property(s => s.Role)
               .HasConversion<string>()
               .HasMaxLength(20);
    }
}