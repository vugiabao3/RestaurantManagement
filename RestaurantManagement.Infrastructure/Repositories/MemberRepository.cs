using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Interfaces.Repositories;
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

    public async Task AddAsync(MemberCard memberCard, CancellationToken cancellationToken)
    {
        await _context.MemberCards.AddAsync(memberCard, cancellationToken);
    }

    public async Task<MemberCard?> GetByByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        return await _context.MemberCards.FirstOrDefaultAsync(m => m.PhoneNumber == phoneNumber, cancellationToken);
    }

    public async Task<MemberCard?> GetByCardIdAsync(string cardId, CancellationToken cancellationToken)
    {
        return await _context.MemberCards.FirstOrDefaultAsync(m => m.CardId == cardId, cancellationToken);
    }

    public async Task<bool> IsPhoneNumberExistsAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        return await _context.MemberCards.AnyAsync(m => m.PhoneNumber == phoneNumber, cancellationToken);
    }

    public async Task<MemberCard?> GetByIdAsync(int memberId, CancellationToken cancellationToken)
    {
        return await _context.MemberCards.FirstOrDefaultAsync(m => m.MemberId == memberId, cancellationToken);
    }

    public void Delete(MemberCard memberCard)
    {
        _context.MemberCards.Remove(memberCard);
    }

    public IQueryable<MemberCard> GetQueryable()
    {
        return _context.MemberCards;
    }
}