using MediatR;

namespace RestaurantManagement.Application.Orders.Commands.PlaceOrder;

public class PlaceOrderCommand
    : IRequest<PlaceOrderResponse>
{
    public int CustomerId { get; set; }
}