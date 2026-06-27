using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Interfaces.Repositories;

public interface IOrderRepository
{
    // Lấy đơn hàng kèm theo chi tiết món ăn và bàn để chuẩn bị thanh toán
    Task<CustomerOrder?> GetOrderForPaymentAsync(int orderId, CancellationToken cancellationToken);
    
    void Update(CustomerOrder order);
}