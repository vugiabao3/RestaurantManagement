using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Interfaces
{
    public interface IStatusHistoryRepository
    {
        Task<List<StatusHistory>> GetByOrderIdAsync(int orderId);
    }
}
