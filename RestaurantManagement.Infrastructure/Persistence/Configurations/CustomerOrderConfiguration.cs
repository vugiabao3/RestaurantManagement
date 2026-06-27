using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Infrastructure.Persistence.Configurations;

public class CustomerOrderConfiguration : IEntityTypeConfiguration<CustomerOrder>
{
    public void Configure(EntityTypeBuilder<CustomerOrder> builder)
    {
        // Đã sửa lại tên bảng thành CustomerOrder giữ tính toàn vẹn của ERD nhóm chốt
        builder.ToTable("CustomerOrder"); 
        
        builder.HasKey(o => o.OrderId);
        
        builder.Property(o => o.OrderStatus)
               .HasConversion<string>()
               .HasMaxLength(20);
               
        builder.HasOne(o => o.DiningTable)
               .WithMany(t => t.Orders)
               .HasForeignKey(o => o.TableId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}