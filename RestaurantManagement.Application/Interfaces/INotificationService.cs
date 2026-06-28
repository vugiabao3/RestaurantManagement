namespace RestaurantManagement.Application.Interfaces
{
    public interface INotificationService
    {
        Task<bool> NotifyOrderCreatedAsync(int orderId, CancellationToken cancellationToken = default);
        Task<bool> NotifyOrderUpdatedAsync(int orderId, CancellationToken cancellationToken = default);
        Task<bool> NotifyOrderDetailStatusChangedAsync(
            int orderId,
            int orderDetailId,
            string status,
            int version,
            string orderStatus,
            int orderVersion,
            bool canCompleteOrder,
            CancellationToken cancellationToken = default);
        Task<bool> NotifyOrderReadyAsync(int orderId, CancellationToken cancellationToken = default);
        Task<bool> NotifyOrderCancelledAsync(int orderId, string reason, CancellationToken cancellationToken = default);
        Task<bool> NotifyMenuAvailabilityChangedAsync(int menuItemId, bool isAvailable, CancellationToken cancellationToken = default);
        Task<bool> NotifyMenuItemChangedAsync(int menuItemId, string changeType, CancellationToken cancellationToken = default);
        Task<bool> NotifyMenuItemDeletedAsync(int menuItemId, CancellationToken cancellationToken = default);
        Task<bool> NotifyPriorityChangedAsync(int orderId, int priority, CancellationToken cancellationToken = default);
    }
}
