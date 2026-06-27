using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Infrastructure.Persistence.Configurations;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.ToTable("OrderDetail");
        
        // Thiết lập Khóa chính gộp (Composite Key)
        builder.HasKey(od => new { od.OrderId, od.ItemId });
        
        builder.Property(od => od.HistoricalPrice)
               .HasColumnType("decimal(18,2)");
               
        builder.Property(od => od.ItemStatus)
               .HasConversion<string>()
               .HasMaxLength(20);
               
        builder.Property(od => od.SpecialNote)
               .HasMaxLength(255)
               .IsRequired(false);
               
        // Quan hệ với CustomerOrder
        builder.HasOne(od => od.Order)
               .WithMany(o => o.OrderDetails)
               .HasForeignKey(od => od.OrderId)
               .OnDelete(DeleteBehavior.Cascade);
               
        // Quan hệ với MenuItem
        builder.HasOne(od => od.MenuItem)
               .WithMany(m => m.OrderDetails)
               .HasForeignKey(od => od.ItemId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}