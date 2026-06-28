using MediatR;
using RestaurantManagement.Application.Common.Exceptions;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.MenuItems.Commands.ToggleMenuItemAvailability
{
    public class ToggleMenuItemAvailabilityHandler
        : IRequestHandler<ToggleMenuItemAvailabilityCommand, ToggleMenuItemAvailabilityResponse>
    {
        private readonly IMenuItemRepository _items;
        private readonly IAuditLogService _audit;
        private readonly INotificationService _notification;
        private readonly ICacheService _cache;

        public ToggleMenuItemAvailabilityHandler(
            IMenuItemRepository items,
            IAuditLogService audit,
            INotificationService notification,
            ICacheService cache)
        {
            _items = items;
            _audit = audit;
            _notification = notification;
            _cache = cache;
        }

        public async Task<ToggleMenuItemAvailabilityResponse> Handle(
            ToggleMenuItemAvailabilityCommand request,
            CancellationToken cancellationToken)
        {
            var item = await _items.GetByIdAsync(request.MenuItemId)
                ?? throw new NotFoundException("Không tìm thấy món ăn");

            item.IsAvailable = request.IsAvailable;

            await _items.UpdateAsync(item);
            await _audit.AddAsync(
                "CHANGE_MENU_AVAILABILITY",
                "MenuItem",
                item.MenuItemId.ToString(),
                request.IsAvailable ? "Còn món" : "Hết món",
                cancellationToken);
            await _items.SaveChangesAsync();

            _cache.RemoveByPrefix("menu:");
            _cache.RemoveByPrefix("pending-orders:");

            var realtimeSynchronized =
                await _notification.NotifyMenuAvailabilityChangedAsync(
                    item.MenuItemId,
                    item.IsAvailable,
                    cancellationToken);

            return new ToggleMenuItemAvailabilityResponse
            {
                Message = realtimeSynchronized
                    ? "Cập nhật thực đơn thành công"
                    : "Đã cập nhật trạng thái món nhưng đồng bộ thời gian thực thất bại",
                IsAvailable = item.IsAvailable,
                RealtimeSynchronized = realtimeSynchronized
            };
        }
    }
}
