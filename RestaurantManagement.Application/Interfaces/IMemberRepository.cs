using System.Linq; // <-- Thêm dòng này
using System.Threading;
using System.Threading.Tasks;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Interfaces.Repositories;

public interface IMemberRepository
{
    Task AddAsync(MemberCard memberCard, CancellationToken cancellationToken);
    Task<MemberCard?> GetByByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken);
    Task<MemberCard?> GetByCardIdAsync(string cardId, CancellationToken cancellationToken);
    Task<bool> IsPhoneNumberExistsAsync(string phoneNumber, CancellationToken cancellationToken);
    
    // <-- BỔ SUNG 3 DÒNG DƯỚI ĐÂY
    Task<MemberCard?> GetByIdAsync(int memberId, CancellationToken cancellationToken);
    void Delete(MemberCard memberCard);
    IQueryable<MemberCard> GetQueryable();
}