using MediatR;

namespace RestaurantManagement.Application.Orders.Commands.UpdateOrderStatus;

public class UpdateOrderStatusCommand
    : IRequest<UpdateOrderStatusResponse>
{
    public int OrderId { get; set; }

    public string Status { get; set; } = string.Empty;
}