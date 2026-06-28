using MediatR;
using RestaurantManagement.Application.Common.Exceptions;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.MenuItems.Commands.DeleteMenuItem
{
    public class DeleteMenuItemHandler : IRequestHandler<DeleteMenuItemCommand, DeleteMenuItemResponse>
    {
        private readonly IMenuItemRepository _items;
        private readonly IAuditLogService _audit;
        private readonly INotificationService _notification;
        private readonly ICacheService _cache;

        public DeleteMenuItemHandler(
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

        public async Task<DeleteMenuItemResponse> Handle(
            DeleteMenuItemCommand request,
            CancellationToken cancellationToken)
        {
            var item = await _items.GetByIdAsync(request.MenuItemId)
                ?? throw new NotFoundException("Không tìm thấy món ăn");

            try
            {
                await _items.DeleteAsync(item);
                await _audit.AddAsync(
                    "DELETE_MENU_ITEM",
                    "MenuItem",
                    item.MenuItemId.ToString(),
                    $"Xóa món {item.Name}",
                    cancellationToken);
                await _items.SaveChangesAsync();
            }
            catch (Exception ex) when (
                ex.GetType().Name.Contains("DbUpdateException", StringComparison.Ordinal))
            {
                throw new BusinessException(
                    "Không thể xóa món đã phát sinh trong Order. Hãy chuyển món sang trạng thái Hết món");
            }

            _cache.RemoveByPrefix("menu:");
            _cache.RemoveByPrefix("pending-orders:");

            var realtimeSynchronized =
                await _notification.NotifyMenuItemDeletedAsync(
                    item.MenuItemId,
                    cancellationToken);

            return new DeleteMenuItemResponse
            {
                Message = realtimeSynchronized
                    ? "Xóa món ăn thành công"
                    : "Đã xóa món nhưng đồng bộ thực đơn thời gian thực thất bại",
                RealtimeSynchronized = realtimeSynchronized
            };
        }
    }
}
