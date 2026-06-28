using MediatR;

namespace RestaurantManagement.Application.Orders.Commands.NotifyOrderCreated
{
    /// <summary>
    /// Điểm tích hợp để module gọi món báo cho màn hình bếp khi một Order mới
    /// đã được lưu thành công.
    /// </summary>
    public class NotifyOrderCreatedCommand : IRequest<NotifyOrderCreatedResponse>
    {
        public int OrderId { get; set; }
    }
}
