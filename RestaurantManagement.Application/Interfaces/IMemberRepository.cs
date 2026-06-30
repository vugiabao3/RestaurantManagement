using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Interfaces;

public interface IMemberRepository
{
    Task<MemberCard?> GetByIdAsync(int memberId);

    Task<MemberCard?> GetByPhoneAsync(string phone);

    Task AddAsync(MemberCard member);

    Task UpdateAsync(MemberCard member);
}