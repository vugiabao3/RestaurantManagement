using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Infrastructure.Persistence.Configurations;

public class MemberCardConfiguration : IEntityTypeConfiguration<MemberCard>
{
    public void Configure(EntityTypeBuilder<MemberCard> builder)
    {
        builder.ToTable("MemberCard");
        
        builder.HasKey(m => m.MemberId);
        
        builder.Property(m => m.FullName)
               .IsRequired()
               .HasMaxLength(100);
               
        builder.Property(m => m.PhoneNumber)
               .IsRequired()
               .HasMaxLength(15);
               
        builder.HasIndex(m => m.PhoneNumber)
               .IsUnique(); // Ràng buộc UNIQUE theo yêu cầu ERD
               
        builder.Property(m => m.CardId)
               .HasMaxLength(255)
               .IsRequired(false); // Cột này Nullable (N)
               
        builder.HasIndex(m => m.CardId)
               .IsUnique()
               .HasFilter("[CardId] IS NOT NULL"); // UNIQUE nhưng cho phép nhiều NULL
    }
}