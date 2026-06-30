using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(Order order);

    Task<Order?> GetByIdAsync(
        int orderId);

    Task UpdateAsync(Order order);

    Task DeleteAsync(Order order);
    Task<Order?> GetCartByCustomerAsync(
    int customerId);

    Task<List<Order>> GetPendingOrdersAsync();
    Task<Order?> GetPaymentOrderAsync(int orderId);

}