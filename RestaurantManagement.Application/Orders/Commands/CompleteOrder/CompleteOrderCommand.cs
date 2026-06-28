using MediatR;

namespace RestaurantManagement.Application.Orders.Commands.CompleteOrder
{
    /// <summary>
    /// UC_KIT_04 - Main Flow: Đầu bếp trưởng bấm "Hoàn thành Order"
    /// sau khi tất cả món đã chế biến xong.
    /// </summary>
    public class CompleteOrderCommand
        : IRequest<CompleteOrderResponse>
    {
        public int OrderId { get; set; }
    }
}
