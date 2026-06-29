using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Interfaces.Repositories;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Persistence;

namespace RestaurantManagement.Infrastructure.Repositories;

public class ShiftRepository : IShiftRepository
{
    private readonly AppDbContext _context;

    public ShiftRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Shift shift, CancellationToken cancellationToken)
    {
        await _context.Shifts.AddAsync(shift, cancellationToken);
    }

    public async Task<Shift?> GetActiveShiftAsync(int userId, CancellationToken cancellationToken)
    {
        return await _context.Shifts
            .FirstOrDefaultAsync(s => s.UserId == userId && s.Status == ShiftStatus.Active, cancellationToken);
    }

    public void Update(Shift shift)
    {
        _context.Shifts.Update(shift);
    }
    public async Task<Shift?> GetByIdAsync(int shiftId, CancellationToken cancellationToken)
{
    return await _context.Shifts.FindAsync(new object[] { shiftId }, cancellationToken);
}
}