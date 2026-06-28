using MediatR;
using RestaurantManagement.Application.Common;
using RestaurantManagement.Application.Common.Exceptions;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Orders.Commands.CompleteOrder
{
    public class CompleteOrderHandler : IRequestHandler<CompleteOrderCommand, CompleteOrderResponse>
    {
        private readonly IOrderRepository _repository;
        private readonly IStatusHistoryService _history;
        private readonly IAuditLogService _audit;
        private readonly INotificationService _notification;
        private readonly ICacheService _cache;

        public CompleteOrderHandler(
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

        public async Task<CompleteOrderResponse> Handle(CompleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetOrderWithDetailsAsync(request.OrderId)
                ?? throw new NotFoundException("Không tìm thấy Order");

            // UC_KIT_04 - BR01: luôn quét lại toàn bộ món ngay trước khi chốt.
            if (!order.IsAllItemsReady())
                throw new BusinessException("Chưa thể chốt đơn. Vẫn còn món chưa hoàn thành");

            StatusTransitionRules.EnsureOrderTransition(order.Status, OrderStatus.Ready);

            var oldStatus = order.Status;
            order.Status = OrderStatus.Ready;
            order.Completed = DateTime.Now;
            order.Version += 1;

            await _history.AddAsync(
                order.OrderId,
                null,
                oldStatus,
                order.Status,
                cancellationToken: cancellationToken);
            await _audit.AddAsync(
                "COMPLETE_ORDER",
                "Order",
                order.OrderId.ToString(),
                "Chốt Order sẵn sàng phục vụ",
                cancellationToken);

            // Order.Version là concurrency token. Nếu thiết bị khác vừa Undo/cập nhật món,
            // thao tác đó cũng tăng Order.Version và lần lưu này sẽ nhận HTTP 409.
            await _repository.SaveChangesAsync();

            _cache.RemoveByPrefix("pending-orders:");
            _cache.RemoveByPrefix("analytics:");

            var staffNotified =
                await _notification.NotifyOrderReadyAsync(order.OrderId, cancellationToken);

            return new CompleteOrderResponse
            {
                Message = staffNotified
                    ? "Cập nhật trạng thái Order thành công"
                    : "Order đã sẵn sàng nhưng không thể gửi thông báo cho phục vụ do lỗi mạng",
                OrderId = order.OrderId,
                Status = order.Status,
                Version = order.Version,
                CompletedAt = order.Completed,
                StaffNotified = staffNotified
            };
        }
    }
}
