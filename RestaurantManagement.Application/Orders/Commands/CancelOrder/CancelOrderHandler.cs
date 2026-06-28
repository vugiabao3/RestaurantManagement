using MediatR;
using RestaurantManagement.Application.Common;
using RestaurantManagement.Application.Common.Exceptions;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Orders.Commands.CancelOrder
{
    public class CancelOrderHandler : IRequestHandler<CancelOrderCommand, CancelOrderResponse>
    {
        private readonly IOrderRepository _repository;
        private readonly IStatusHistoryService _history;
        private readonly IAuditLogService _audit;
        private readonly INotificationService _notification;
        private readonly ICacheService _cache;

        public CancelOrderHandler(
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

        public async Task<CancelOrderResponse> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Reason))
                throw new BusinessException("Vui lòng nhập lý do lỗi Order");

            var order = await _repository.GetOrderWithDetailsAsync(request.OrderId)
                ?? throw new NotFoundException("Không tìm thấy Order");

            StatusTransitionRules.EnsureOrderTransition(order.Status, OrderStatus.Cancelled);

            var oldStatus = order.Status;
            order.Status = OrderStatus.Cancelled;
            order.CancelReason = request.Reason.Trim();
            order.Version += 1;

            await _history.AddAsync(
                order.OrderId,
                null,
                oldStatus,
                order.Status,
                order.CancelReason,
                cancellationToken);
            await _audit.AddAsync(
                "CANCEL_ORDER",
                "Order",
                order.OrderId.ToString(),
                $"Hủy Order: {order.CancelReason}",
                cancellationToken);

            await _repository.SaveChangesAsync();

            _cache.RemoveByPrefix("pending-orders:");
            _cache.RemoveByPrefix("analytics:");

            var notificationSent =
                await _notification.NotifyOrderCancelledAsync(
                    order.OrderId,
                    order.CancelReason,
                    cancellationToken);

            return new CancelOrderResponse
            {
                Message = notificationSent
                    ? "Đã hủy Order và báo khẩn cho nhân viên phục vụ"
                    : "Đã hủy Order nhưng không thể gửi thông báo do lỗi mạng",
                OrderId = order.OrderId,
                Status = order.Status,
                Version = order.Version,
                NotificationSent = notificationSent
            };
        }
    }
}
