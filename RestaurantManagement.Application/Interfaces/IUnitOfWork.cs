using System.Threading;
using System.Threading.Tasks;
using RestaurantManagement.Application.Interfaces.Repositories; // <-- Dòng mới được thêm

namespace RestaurantManagement.Application.Interfaces;

public interface IUnitOfWork
{
    IOrderRepository OrderRepository { get; }
    IInvoiceRepository InvoiceRepository { get; }
    IMemberRepository MemberRepository { get; }
    IShiftRepository ShiftRepository { get; }
    IOrderRepository Orders { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}