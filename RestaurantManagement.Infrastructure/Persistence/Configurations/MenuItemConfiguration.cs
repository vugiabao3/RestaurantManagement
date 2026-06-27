using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Infrastructure.Persistence.Configurations;

public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.ToTable("MenuItem");
        
        builder.HasKey(m => m.ItemId);
        
        builder.Property(m => m.ItemName)
               .IsRequired()
               .HasMaxLength(150);
               
        builder.Property(m => m.CurrentPrice)
               .HasColumnType("decimal(18,2)")
               .IsRequired();
               
        builder.Property(m => m.ImageUrl)
               .HasMaxLength(255);
               
        builder.Property(m => m.IsAvailable)
               .HasDefaultValue(true);

        // Đã bổ sung cấu hình quan hệ N-1 với Category đúng chuẩn ERD
        builder.HasOne(m => m.Category)
               .WithMany(c => c.MenuItems)
               .HasForeignKey(m => m.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}