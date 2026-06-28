namespace RestaurantManagement.Application.Interfaces
{
    public interface IStatusHistoryService
    {
        Task AddAsync(int? orderId, int? orderDetailId, string oldStatus, string newStatus, string? reason = null, CancellationToken cancellationToken = default);
    }
}
