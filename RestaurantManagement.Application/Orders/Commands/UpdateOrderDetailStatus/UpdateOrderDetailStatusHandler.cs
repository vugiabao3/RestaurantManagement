using MediatR;
using RestaurantManagement.Application.Common;
using RestaurantManagement.Application.Common.Exceptions;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Orders.Commands.UpdateOrderDetailStatus
{
    public class UpdateOrderDetailStatusHandler
        : IRequestHandler<UpdateOrderDetailStatusCommand, UpdateOrderDetailStatusResponse>
    {
        private readonly IOrderRepository _repository;
        private readonly IStatusHistoryService _history;
        private readonly IAuditLogService _audit;
        private readonly INotificationService _notification;
        private readonly ICacheService _cache;

        public UpdateOrderDetailStatusHandler(
            IOrderRepository repository,
            IStatusHistoryService history,
            IAuditLogService audit,
            INotificationService notification,
            ICacheService cache)
        {
            _repository = repository;
            _history = history;
            _audit = audit;
            _notification = notification;
            _cache = cache;
        }

        public async Task<UpdateOrderDetailStatusResponse> Handle(
            UpdateOrderDetailStatusCommand request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Status))
                throw new BusinessException("Trạng thái món không hợp lệ");

            var detail = await _repository.GetOrderDetailWithContextAsync(request.OrderDetailId)
                ?? throw new NotFoundException("Không tìm thấy món ăn trong Order");

            if (detail.Order == null)
                throw new BusinessException("Món ăn không thuộc Order hợp lệ");

            if (detail.Order.Status == OrderStatus.Ready ||
                detail.Order.Status == OrderStatus.Completed ||
                detail.Order.Status == OrderStatus.Cancelled)
            {
                throw new BusinessException("Không thể cập nhật món của Order đã kết thúc");
            }

            if (detail.Version != request.Version)
                throw new ConflictException("Dữ liệu đang được xử lý bởi máy khác, vui lòng làm mới");

            var newStatus = request.Status.Trim().ToUpperInvariant();
            StatusTransitionRules.EnsureOrderDetailTransition(detail.Status, newStatus);

            var oldStatus = detail.Status;
            detail.PreviousStatus = oldStatus;
            detail.Status = newStatus;
            detail.Version += 1;

            // Tài liệu không có Use Case tiếp nhận Order riêng. Khi món đầu tiên bắt đầu nấu,
            // Order tự chuyển PENDING -> PREPARING.
            if (detail.Order.Status == OrderStatus.Pending && newStatus == OrderStatus.Preparing)
            {
                StatusTransitionRules.EnsureOrderTransition(
                    detail.Order.Status,
                    OrderStatus.Preparing);

                var oldOrderStatus = detail.Order.Status;
                detail.Order.Status = OrderStatus.Preparing;
                await _history.AddAsync(
                    detail.OrderId,
                    null,
                    oldOrderStatus,
                    detail.Order.Status,
                    "Tự động chuyển khi món đầu tiên bắt đầu chế biến",
                    cancellationToken);
            }

            // Tăng version cấp Order để Complete Order xung đột nếu thiết bị khác vừa
            // cập nhật hoặc Undo một món.
            detail.Order.Version += 1;

            if (newStatus == OrderStatus.OutOfStock && detail.MenuItem != null)
                detail.MenuItem.IsAvailable = false;

            await _history.AddAsync(
                detail.OrderId,
                detail.OrderDetailId,
                oldStatus,
                newStatus,
                cancellationToken: cancellationToken);
            await _audit.AddAsync(
                "CHANGE_ITEM_STATUS",
                "OrderDetail",
                detail.OrderDetailId.ToString(),
                $"{oldStatus} -> {newStatus}",
                cancellationToken);

            await _repository.SaveChangesAsync();

            _cache.RemoveByPrefix("pending-orders:");
            _cache.RemoveByPrefix("menu:");
            _cache.RemoveByPrefix("analytics:");

            var canCompleteOrder = detail.Order.IsAllItemsReady();
            var statusSynchronized =
                await _notification.NotifyOrderDetailStatusChangedAsync(
                    detail.OrderId,
                    detail.OrderDetailId,
                    detail.Status,
                    detail.Version,
                    detail.Order.Status,
                    detail.Order.Version,
                    canCompleteOrder,
                    cancellationToken);

            var menuSynchronized = true;
            if (newStatus == OrderStatus.OutOfStock && detail.MenuItem != null)
            {
                menuSynchronized =
                    await _notification.NotifyMenuAvailabilityChangedAsync(
                        detail.MenuItemId,
                        false,
                        cancellationToken);
            }

            var realtimeSynchronized = statusSynchronized && menuSynchronized;

            return new UpdateOrderDetailStatusResponse
            {
                Message = realtimeSynchronized
                    ? $"Cập nhật trạng thái {newStatus} thành công"
                    : $"Đã cập nhật trạng thái {newStatus} nhưng đồng bộ thời gian thực thất bại",
                OrderDetailId = detail.OrderDetailId,
                Status = detail.Status,
                Version = detail.Version,
                OrderVersion = detail.Order.Version,
                CanCompleteOrder = canCompleteOrder,
                RealtimeSynchronized = realtimeSynchronized
            };
        }
    }
}
