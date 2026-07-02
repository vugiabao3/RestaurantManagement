using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Interfaces;

public interface IMemberRepository
{
    Task<MemberCard?> GetByIdAsync(int memberId);

    Task<MemberCard?> GetByUserIdAsync(int userId);
    Task AddAsync(MemberCard member);

    Task UpdateAsync(MemberCard member);
}