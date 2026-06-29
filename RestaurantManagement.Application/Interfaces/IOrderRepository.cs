using System.Linq; // <-- Thêm dòng này để dùng được IQueryable
using System.Threading;
using System.Threading.Tasks;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<CustomerOrder?> GetOrderForPaymentAsync(int orderId, CancellationToken cancellationToken);
    Task AddAsync(CustomerOrder order, CancellationToken cancellationToken);
    Task<IEnumerable<CustomerOrder>> GetPaidOrdersByMonthAsync(int month, int year);
    void Update(CustomerOrder order);
    IQueryable<CustomerOrder> GetQueryable(); 
}