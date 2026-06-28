using MediatR;
using RestaurantManagement.Application.Common.Exceptions;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Orders.Commands.NotifyOrderCreated
{
    public class NotifyOrderCreatedHandler
        : IRequestHandler<NotifyOrderCreatedCommand, NotifyOrderCreatedResponse>
    {
        private readonly IOrderRepository _orders;
        private readonly INotificationService _notification;
        private readonly ICacheService _cache;

        public NotifyOrderCreatedHandler(
            IOrderRepository orders,
            INotificationService notification,
            ICacheService cache)
        {
            _orders = orders;
            _notification = notification;
            _cache = cache;
        }

        public async Task<NotifyOrderCreatedResponse> Handle(
            NotifyOrderCreatedCommand request,
            CancellationToken cancellationToken)
        {
            var order = await _orders.GetOrderWithDetailsAsync(request.OrderId)
                ?? throw new NotFoundException("Không tìm thấy Order");

            if (order.Status != OrderStatus.Pending)
                throw new BusinessException("Chỉ phát tín hiệu Order mới khi Order đang ở trạng thái PENDING");

            if (order.OrderDetails.Count == 0)
                throw new BusinessException("Order chưa có món ăn để gửi tới bếp");

            _cache.RemoveByPrefix("pending-orders:");
            _cache.RemoveByPrefix("analytics:");

            var realtimeSynchronized =
                await _notification.NotifyOrderCreatedAsync(order.OrderId, cancellationToken);

            return new NotifyOrderCreatedResponse
            {
                Message = realtimeSynchronized
                    ? "Đã gửi Order mới tới màn hình bếp"
                    : "Order đã được lưu nhưng chưa thể gửi tín hiệu tới màn hình bếp",
                OrderId = order.OrderId,
                RealtimeSynchronized = realtimeSynchronized
            };
        }
    }
}
