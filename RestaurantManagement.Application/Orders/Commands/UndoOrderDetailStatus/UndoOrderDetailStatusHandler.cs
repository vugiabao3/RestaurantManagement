using MediatR;
using RestaurantManagement.Application.Common.Exceptions;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Orders.Commands.UndoOrderDetailStatus
{
    public class UndoOrderDetailStatusHandler
        : IRequestHandler<UndoOrderDetailStatusCommand, UndoOrderDetailStatusResponse>
    {
        private readonly IOrderRepository _repository;
        private readonly IStatusHistoryService _history;
        private readonly IAuditLogService _audit;
        private readonly INotificationService _notification;
        private readonly ICacheService _cache;

        public UndoOrderDetailStatusHandler(
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

        public async Task<UndoOrderDetailStatusResponse> Handle(
            UndoOrderDetailStatusCommand request,
            CancellationToken cancellationToken)
        {
            var detail = await _repository.GetOrderDetailWithContextAsync(request.OrderDetailId)
                ?? throw new NotFoundException("Không tìm thấy món ăn trong Order");

            if (detail.Order == null)
                throw new BusinessException("Món ăn không thuộc Order hợp lệ");

            if (detail.Order.Status == OrderStatus.Ready ||
                detail.Order.Status == OrderStatus.Completed ||
                detail.Order.Status == OrderStatus.Cancelled)
            {
                throw new BusinessException("Không thể hoàn tác món của Order đã kết thúc");
            }

            if (detail.Version != request.Version)
                throw new ConflictException("Dữ liệu đang được xử lý bởi máy khác, vui lòng làm mới");

            if (string.IsNullOrWhiteSpace(detail.PreviousStatus))
                throw new BusinessException("Không có thao tác nào để hoàn tác");

            var oldStatus = detail.Status;
            var restoredStatus = detail.PreviousStatus;

            if (oldStatus == OrderStatus.OutOfStock && detail.MenuItem != null)
                detail.MenuItem.IsAvailable = true;

            detail.Status = restoredStatus;
            detail.PreviousStatus = null;
            detail.Version += 1;
            detail.Order.Version += 1;

            await _history.AddAsync(
                detail.OrderId,
                detail.OrderDetailId,
                oldStatus,
                restoredStatus,
                "UNDO",
                cancellationToken);
            await _audit.AddAsync(
                "UNDO_ITEM_STATUS",
                "OrderDetail",
                detail.OrderDetailId.ToString(),
                $"Hoàn tác {oldStatus} -> {restoredStatus}",
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
            if (oldStatus == OrderStatus.OutOfStock && detail.MenuItem != null)
            {
                menuSynchronized =
                    await _notification.NotifyMenuAvailabilityChangedAsync(
                        detail.MenuItemId,
                        true,
                        cancellationToken);
            }

            var realtimeSynchronized = statusSynchronized && menuSynchronized;

            return new UndoOrderDetailStatusResponse
            {
                Message = realtimeSynchronized
                    ? "Hoàn tác thành công"
                    : "Đã hoàn tác nhưng đồng bộ thời gian thực thất bại",
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
