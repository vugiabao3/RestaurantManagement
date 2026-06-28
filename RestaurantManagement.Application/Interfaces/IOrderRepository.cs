using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetPendingOrdersAsync(int? kitchenAreaId = null);
        Task<Order?> GetOrderWithDetailsAsync(int orderId);
        Task<OrderDetail?> GetOrderDetailWithContextAsync(int orderDetailId);
        Task SaveChangesAsync();
    }
}
