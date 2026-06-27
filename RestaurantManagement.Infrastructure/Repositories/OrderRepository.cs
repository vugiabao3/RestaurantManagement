using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Interfaces.Repositories;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Persistence;

namespace RestaurantManagement.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CustomerOrder?> GetOrderForPaymentAsync(int orderId, CancellationToken cancellationToken)
    {
        return await _context.CustomerOrders
            .Include(o => o.OrderDetails)
                .ThenInclude(od => od.MenuItem)
            .Include(o => o.DiningTable)
            .FirstOrDefaultAsync(o => o.OrderId == orderId, cancellationToken);
    }

    public void Update(CustomerOrder order)
    {
        _context.CustomerOrders.Update(order);
    }
}