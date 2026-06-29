using System.Threading;
using System.Threading.Tasks;
using RestaurantManagement.Domain.Entities; // Cần có thực thể Shift

namespace RestaurantManagement.Application.Interfaces.Repositories;

public interface IShiftRepository
{
    Task AddAsync(Shift shift, CancellationToken cancellationToken);
    Task<Shift?> GetActiveShiftAsync(int userId, CancellationToken cancellationToken);
    Task<Shift?> GetByIdAsync(int shiftId, CancellationToken cancellationToken);
    void Update(Shift shift);
}