using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Infrastructure.Persistence;

namespace RestaurantManagement.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        // Tại đây sau này có thể chèn thêm logic Dispatch Domain Events nếu hệ thống mở rộng
        return await _context.SaveChangesAsync(cancellationToken);
    }
}