using MediatR;
using RestaurantManagement.Application.Common.Exceptions;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.MenuItems.Commands.UpdateMenuItem
{
    public class UpdateMenuItemHandler : IRequestHandler<UpdateMenuItemCommand, UpdateMenuItemResponse>
    {
        private readonly IMenuItemRepository _items;
        private readonly IKitchenAreaRepository _areas;
        private readonly IAuditLogService _audit;
        private readonly INotificationService _notification;
        private readonly ICacheService _cache;

        public UpdateMenuItemHandler(
            IMenuItemRepository items,
            IKitchenAreaRepository areas,
            IAuditLogService audit,
            INotificationService notification,
            ICacheService cache)
        {
            _items = items;
            _areas = areas;
            _audit = audit;
            _notification = notification;
            _cache = cache;
        }

        public async Task<UpdateMenuItemResponse> Handle(
            UpdateMenuItemCommand request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new BusinessException("Tên món ăn không được để trống");
            if (string.IsNullOrWhiteSpace(request.Category))
                throw new BusinessException("Danh mục món ăn không được để trống");
            if (request.Price <= 0)
                throw new BusinessException("Giá bán phải lớn hơn 0");

            var item = await _items.GetByIdAsync(request.MenuItemId)
                ?? throw new NotFoundException("Không tìm thấy món ăn");

            if (request.KitchenAreaId.HasValue &&
                await _areas.GetByIdAsync(request.KitchenAreaId.Value) == null)
            {
                throw new NotFoundException("Không tìm thấy khu vực bếp");
            }

            item.Name = request.Name.Trim();
            item.Category = request.Category.Trim();
            item.Price = request.Price;
            item.IsAvailable = request.IsAvailable;
            item.KitchenAreaId = request.KitchenAreaId;

            await _items.UpdateAsync(item);
            await _audit.AddAsync(
                "UPDATE_MENU_ITEM",
                "MenuItem",
                item.MenuItemId.ToString(),
                $"Cập nhật món {item.Name}",
                cancellationToken);
            await _items.SaveChangesAsync();

            _cache.RemoveByPrefix("menu:");
            _cache.RemoveByPrefix("pending-orders:");

            var realtimeSynchronized =
                await _notification.NotifyMenuItemChangedAsync(
                    item.MenuItemId,
                    "UPDATED",
                    cancellationToken);

            return new UpdateMenuItemResponse
            {
                Message = realtimeSynchronized
                    ? "Cập nhật thực đơn thành công"
                    : "Đã cập nhật món nhưng đồng bộ thực đơn thời gian thực thất bại",
                RealtimeSynchronized = realtimeSynchronized
            };
        }
    }
}
