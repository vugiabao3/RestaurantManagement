using MediatR;

namespace RestaurantManagement.Application.Orders.Commands.AcceptOrder
{
    /// <summary>
    /// UC_KIT_04 - Alt [Accept Order]: Đầu bếp xác nhận tiếp nhận Order,
    /// chuyển trạng thái Order từ PENDING -> PREPARING.
    /// </summary>
    public class AcceptOrderCommand
        : IRequest<AcceptOrderResponse>
    {
        public int OrderId { get; set; }
    }
}
