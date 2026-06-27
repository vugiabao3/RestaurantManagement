using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Infrastructure.Persistence.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoice");
        
        builder.HasKey(i => i.InvoiceId);
        
        builder.Property(i => i.SubTotalAmount).HasColumnType("decimal(18,2)");
        builder.Property(i => i.DiscountAmount).HasColumnType("decimal(18,2)");
        builder.Property(i => i.FinalAmount).HasColumnType("decimal(18,2)");

        builder.Property(i => i.PaymentMethod)
               .HasConversion<string>()
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(i => i.DiscountReason)
               .HasMaxLength(255)
               .IsRequired(false); 

        // Quan hệ 1-1 với CustomerOrder
        builder.HasOne(i => i.Order)
               .WithOne(o => o.Invoice)
               .HasForeignKey<Invoice>(i => i.OrderId)
               .OnDelete(DeleteBehavior.Restrict);
               
        // Đã cập nhật map sang s.Invoices ngắn gọn hơn theo review
        builder.HasOne(i => i.Cashier)
               .WithMany(s => s.Invoices)
               .HasForeignKey(i => i.CashierId)
               .OnDelete(DeleteBehavior.Restrict);
               
        // Quan hệ N-1 với MemberCard
        builder.HasOne(i => i.Member)
               .WithMany(m => m.Invoices)
               .HasForeignKey(i => i.MemberId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}