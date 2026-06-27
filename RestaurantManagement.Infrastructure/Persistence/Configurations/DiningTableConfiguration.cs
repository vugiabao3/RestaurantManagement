using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Infrastructure.Persistence.Configurations;

public class DiningTableConfiguration : IEntityTypeConfiguration<DiningTable>
{
    public void Configure(EntityTypeBuilder<DiningTable> builder)
    {
        builder.ToTable("DiningTable");
        
        builder.HasKey(t => t.TableId);
        
        builder.Property(t => t.TableNumber)
               .IsRequired()
               .HasMaxLength(10);
               
        builder.HasIndex(t => t.TableNumber)
               .IsUnique();
               
        builder.Property(t => t.CurrentStatus)
               .HasConversion<string>() // Lưu Enum dưới dạng Text cho dễ đọc trong DB
               .HasMaxLength(20);
    }
}