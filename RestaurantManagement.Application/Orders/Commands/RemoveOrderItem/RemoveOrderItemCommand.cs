using MediatR;

namespace RestaurantManagement.Application.Orders.Commands.RemoveOrderItem;

public class RemoveOrderItemCommand
    : IRequest<RemoveOrderItemResponse>
{
    public int OrderItemId { get; set; }
}