using MediatR;
using RestaurantManagement.Application.Common.Exceptions;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.MenuItems.Commands.CreateMenuItem
{
    public class CreateMenuItemHandler : IRequestHandler<CreateMenuItemCommand, CreateMenuItemResponse>
    {
        private readonly IMenuItemRepository _items;
        private readonly IKitchenAreaRepository _areas;
        private readonly IAuditLogService _audit;
        private readonly INotificationService _notification;
        private readonly ICacheService _cache;

        public CreateMenuItemHandler(
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

        public async Task<CreateMenuItemResponse> Handle(
            CreateMenuItemCommand request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new BusinessException("Tên món ăn không được để trống");
            if (string.IsNullOrWhiteSpace(request.Category))
                throw new BusinessException("Danh mục món ăn không được để trống");
            if (request.Price <= 0)
                throw new BusinessException("Giá bán phải lớn hơn 0");

            if (request.KitchenAreaId.HasValue &&
                await _areas.GetByIdAsync(request.KitchenAreaId.Value) == null)
            {
                throw new NotFoundException("Không tìm thấy khu vực bếp");
            }

            var item = new MenuItem
            {
                Name = request.Name.Trim(),
                Category = request.Category.Trim(),
                Price = request.Price,
                IsAvailable = request.IsAvailable,
                KitchenAreaId = request.KitchenAreaId
            };

            await _items.AddAsync(item);
            await _items.SaveChangesAsync();

            await _audit.AddAsync(
                "CREATE_MENU_ITEM",
                "MenuItem",
                item.MenuItemId.ToString(),
                $"Tạo món {item.Name}",
                cancellationToken);
            await _items.SaveChangesAsync();

            _cache.RemoveByPrefix("menu:");

            var realtimeSynchronized =
                await _notification.NotifyMenuItemChangedAsync(
                    item.MenuItemId,
                    "CREATED",
                    cancellationToken);

            return new CreateMenuItemResponse
            {
                Message = realtimeSynchronized
                    ? "Cập nhật thực đơn thành công"
                    : "Đã thêm món nhưng đồng bộ thực đơn thời gian thực thất bại",
                MenuItemId = item.MenuItemId,
                RealtimeSynchronized = realtimeSynchronized
            };
        }
    }
}
