using MediatR;
using RestaurantManagement.Application.Common;
using RestaurantManagement.Application.Common.Exceptions;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Orders.Commands.AcceptOrder
{
    public class AcceptOrderHandler : IRequestHandler<AcceptOrderCommand, AcceptOrderResponse>
    {
        private readonly IOrderRepository _repository;
        private readonly IStatusHistoryService _history;
        private readonly IAuditLogService _audit;
        private readonly INotificationService _notification;
        private readonly ICacheService _cache;

        public AcceptOrderHandler(
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

        public async Task<AcceptOrderResponse> Handle(AcceptOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetOrderWithDetailsAsync(request.OrderId)
                ?? throw new NotFoundException("Không tìm thấy Order");

            StatusTransitionRules.EnsureOrderTransition(order.Status, OrderStatus.Preparing);

            var oldStatus = order.Status;
            order.Status = OrderStatus.Preparing;
            order.Version += 1;

            await _history.AddAsync(
                order.OrderId,
                null,
                oldStatus,
                order.Status,
                cancellationToken: cancellationToken);
            await _audit.AddAsync(
                "ACCEPT_ORDER",
                "Order",
                order.OrderId.ToString(),
                "Tiếp nhận Order để chế biến",
                cancellationToken);

            await _repository.SaveChangesAsync();

            _cache.RemoveByPrefix("pending-orders:");
            _cache.RemoveByPrefix("analytics:");

            var realtimeSynchronized =
                await _notification.NotifyOrderUpdatedAsync(order.OrderId, cancellationToken);

            return new AcceptOrderResponse
            {
                Message = realtimeSynchronized
                    ? "Đã tiếp nhận Order, chuyển sang chế biến"
                    : "Đã tiếp nhận Order nhưng chưa thể đồng bộ thời gian thực",
                OrderId = order.OrderId,
                Status = order.Status,
                Version = order.Version,
                RealtimeSynchronized = realtimeSynchronized
            };
        }
    }
}
