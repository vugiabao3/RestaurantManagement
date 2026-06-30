using MediatR;

namespace RestaurantManagement.Application.Orders.Commands.ClearCart;

public class ClearCartCommand
    : IRequest<ClearCartResponse>
{
    public int CustomerId { get; set; }
}