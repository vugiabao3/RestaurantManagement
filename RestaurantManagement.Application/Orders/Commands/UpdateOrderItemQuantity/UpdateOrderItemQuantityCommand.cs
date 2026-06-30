using MediatR;

namespace RestaurantManagement.Application
.Orders.Commands.UpdateOrderItemQuantity;

public class UpdateOrderItemQuantityCommand
    : IRequest<UpdateOrderItemQuantityResponse>
{
    public int OrderItemId { get; set; }

    public int Quantity { get; set; }
}