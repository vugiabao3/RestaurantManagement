using MediatR;
using RestaurantManagement.Application.Common.Exceptions;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Orders.Commands.UpdateOrderPriority
{
    public class UpdateOrderPriorityHandler
        : IRequestHandler<UpdateOrderPriorityCommand, UpdateOrderPriorityResponse>
    {
        private readonly IOrderRepository _orders;
        private readonly IAuditLogService _audit;
        private readonly INotificationService _notification;
        private readonly ICacheService _cache;

        public UpdateOrderPriorityHandler(
            IOrderRepository orders,
            IAuditLogService audit,
            INotificationService notification,
            ICacheService cache)
        {
            _orders = orders;
            _audit = audit;
            _notification = notification;
            _cache = cache;
        }

        public async Task<UpdateOrderPriorityResponse> Handle(
            UpdateOrderPriorityCommand request,
            CancellationToken cancellationToken)
        {
            var order = await _orders.GetOrderWithDetailsAsync(request.OrderId)
                ?? throw new NotFoundException("Không tìm thấy Order");

            if (order.Status != OrderStatus.Pending && order.Status != OrderStatus.Preparing)
                throw new BusinessException("Chỉ được đổi ưu tiên cho Order đang chờ hoặc đang chế biến");

            order.Priority = request.Priority;
            order.Version += 1;

            await _audit.AddAsync(
                "CHANGE_PRIORITY",
                "Order",
                order.OrderId.ToString(),
                $"Priority = {request.Priority}",
                cancellationToken);
            await _orders.SaveChangesAsync();

            _cache.RemoveByPrefix("pending-orders:");

            var realtimeSynchronized =
                await _notification.NotifyPriorityChangedAsync(
                    order.OrderId,
                    order.Priority,
                    cancellationToken);

            return new UpdateOrderPriorityResponse
            {
                Message = realtimeSynchronized
                    ? "Cập nhật mức ưu tiên thành công"
                    : "Đã cập nhật mức ưu tiên nhưng đồng bộ thời gian thực thất bại",
                OrderId = order.OrderId,
                Priority = order.Priority,
                Version = order.Version,
                RealtimeSynchronized = realtimeSynchronized
            };
        }
    }
}
