using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Persistence;

namespace RestaurantManagement.Infrastructure.Repositories;

public class OrderItemRepository
    : IOrderItemRepository
{
    private readonly AppDbContext
        _context;

    public OrderItemRepository(
        AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        OrderItem item)
    {
        await _context.OrderItems
            .AddAsync(item);

        await _context.SaveChangesAsync();
    }

    public async Task<OrderItem?>
        GetByIdAsync(int itemId)
    {
        return await _context.OrderItems
            .FirstOrDefaultAsync(
                x => x.OrderItemId == itemId);
    }

    public async Task UpdateAsync(
        OrderItem item)
    {
        _context.OrderItems.Update(item);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(
        OrderItem item)
    {
        _context.OrderItems.Remove(item);

        await _context.SaveChangesAsync();
    }
    public async Task<OrderItem?>
GetByOrderAndDishAsync(
    int orderId,
    int dishId)
    {
        return await _context.OrderItems

            .FirstOrDefaultAsync(x =>

                x.OrderId == orderId &&

                x.DishId == dishId);
    }
    public async Task<List<OrderItem>>
GetByOrderIdAsync(
    int orderId)
    {
        return await _context.OrderItems

            .Where(x => x.OrderId == orderId)

            .ToListAsync();
    }
}