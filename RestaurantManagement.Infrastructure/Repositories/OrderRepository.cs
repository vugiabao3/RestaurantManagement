using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Enums;
using RestaurantManagement.Infrastructure.Persistence;

namespace RestaurantManagement.Infrastructure.Repositories;

public class OrderRepository
    : IOrderRepository
{
    private readonly AppDbContext
        _context;

    public OrderRepository(
        AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Order order)
    {
        await _context.Orders
            .AddAsync(order);

        await _context.SaveChangesAsync();
    }

    public async Task<Order?>
        GetByIdAsync(int orderId)
    {
        return await _context.Orders
    .Include(x => x.MemberCard)
    .Include(x => x.OrderItems)
    .FirstOrDefaultAsync(x => x.OrderId == orderId);
    }

    public async Task UpdateAsync(
        Order order)
    {
        _context.Orders.Update(order);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(
        Order order)
    {
        _context.Orders.Remove(order);

        await _context.SaveChangesAsync();
    }
    public async Task<Order> GetCartByCustomerAsync(int customerId)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Dish)
            .FirstOrDefaultAsync(o =>
                o.CustomerId == customerId &&
                o.Status == OrderStatus.Cart);
    }
    public async Task<List<Order>> GetPendingOrdersAsync()
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(i => i.Dish)

            .Where(o =>
                o.Status == "Pending" ||
                o.Status == "Preparing")

            .OrderBy(o => o.CreatedAt)

            .ToListAsync();
    }

    public async Task<Order?> GetPaymentOrderAsync(int orderId)
    {
        return await _context.Orders
            .Include(x => x.MemberCard)
            .Include(x => x.OrderItems)
            .ThenInclude(x => x.Dish)
            .FirstOrDefaultAsync(x => x.OrderId == orderId);
    }


}