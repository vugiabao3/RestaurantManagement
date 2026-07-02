using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Persistence;

namespace RestaurantManagement.Infrastructure.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly AppDbContext _context;

    public MemberRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<MemberCard?> GetByIdAsync(int memberId)
    {
        return await _context.MemberCards
            .Include(x => x.Invoices)
            .FirstOrDefaultAsync(x => x.MemberId == memberId);
    }

    public async Task<MemberCard?> GetByUserIdAsync(int userId)
    {
        return await _context.MemberCards
            .FirstOrDefaultAsync(x => x.UserId == userId);
    }

    public async Task AddAsync(MemberCard member)
    {
        await _context.MemberCards.AddAsync(member);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(MemberCard member)
    {
        _context.MemberCards.Update(member);
        await _context.SaveChangesAsync();
    }
}