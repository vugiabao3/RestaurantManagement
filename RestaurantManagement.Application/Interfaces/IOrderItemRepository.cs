using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Interfaces;

public interface IOrderItemRepository
{
    Task AddAsync(
        OrderItem item);

    Task<OrderItem?> GetByIdAsync(
        int itemId);

    Task UpdateAsync(
        OrderItem item);

    Task DeleteAsync(
        OrderItem item);

    Task<OrderItem?> GetByOrderAndDishAsync(
    int orderId,
    int dishId);

    Task<List<OrderItem>>
GetByOrderIdAsync(
    int orderId);
}