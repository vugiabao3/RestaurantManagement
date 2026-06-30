using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.Orders.Commands.PlaceOrder;

public class PlaceOrderHandler
    : IRequestHandler<
        PlaceOrderCommand,
        PlaceOrderResponse>
{
    private readonly IOrderRepository
        _orderRepository;

    public PlaceOrderHandler(
        IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<PlaceOrderResponse>
        Handle(
            PlaceOrderCommand request,
            CancellationToken cancellationToken)
    {
        var order =
            await _orderRepository
                .GetCartByCustomerAsync(
                    request.CustomerId);

        if (order == null)
            throw new Exception(
                "Cart not found");

        order.Status = "Pending";

        await _orderRepository
            .UpdateAsync(order);

        return new PlaceOrderResponse
        {
            Message =
                "Order sent to kitchen"
        };
    }
}