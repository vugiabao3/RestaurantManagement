using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.Orders.Commands.ClearCart;

public class ClearCartHandler
    : IRequestHandler<
        ClearCartCommand,
        ClearCartResponse>
{
    private readonly IOrderRepository
        _orderRepository;

    private readonly IOrderItemRepository
        _orderItemRepository;

    public ClearCartHandler(

        IOrderRepository orderRepository,

        IOrderItemRepository orderItemRepository)
    {
        _orderRepository = orderRepository;

        _orderItemRepository = orderItemRepository;
    }

    public async Task<ClearCartResponse>
        Handle(
        ClearCartCommand request,
        CancellationToken cancellationToken)
    {
        //------------------------------------------------

        var order =
            await _orderRepository
                .GetCartByCustomerAsync(
                    request.CustomerId);

        if (order == null)
        {
            throw new Exception(
                "Cart not found");
        }

        //------------------------------------------------

        var items =
            await _orderItemRepository
                .GetByOrderIdAsync(
                    order.OrderId);

        //------------------------------------------------

        foreach (var item in items)
        {
            await _orderItemRepository
                .DeleteAsync(item);
        }

        //------------------------------------------------

        order.TotalAmount = 0;

        await _orderRepository
            .UpdateAsync(order);

        //------------------------------------------------

        return new ClearCartResponse
        {
            Message = "Cart cleared"
        };
    }
}