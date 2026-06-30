using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application
.Orders.Commands.UpdateOrderItemQuantity;

public class UpdateOrderItemQuantityHandler
    : IRequestHandler<
        UpdateOrderItemQuantityCommand,
        UpdateOrderItemQuantityResponse>
{
    private readonly IOrderRepository _orderRepository;

    private readonly IOrderItemRepository _orderItemRepository;

    public UpdateOrderItemQuantityHandler(

        IOrderRepository orderRepository,

        IOrderItemRepository orderItemRepository)
    {
        _orderRepository = orderRepository;

        _orderItemRepository = orderItemRepository;
    }

    public async Task<UpdateOrderItemQuantityResponse>
    Handle(

        UpdateOrderItemQuantityCommand request,

        CancellationToken cancellationToken)
    {

        var item =
            await _orderItemRepository
                .GetByIdAsync(request.OrderItemId);

        if (item == null)
            throw new Exception("Order Item not found");

        var order =
            await _orderRepository
                .GetByIdAsync(item.OrderId);

        if (order == null)
            throw new Exception("Order not found");

        //---------------------------------------------------
        // quantity = 0
        //---------------------------------------------------

        if (request.Quantity == 0)
        {
            order.TotalAmount -= item.SubTotal;

            await _orderRepository
                .UpdateAsync(order);

            await _orderItemRepository
                .DeleteAsync(item);

            return new UpdateOrderItemQuantityResponse
            {
                Message = "Dish removed from cart"
            };
        }

        //---------------------------------------------------
        // Update Quantity
        //---------------------------------------------------

        item.Quantity = request.Quantity;

        item.SubTotal =
            item.Quantity * item.UnitPrice;

        await _orderItemRepository
            .UpdateAsync(item);

        order.TotalAmount =
            order.OrderItems.Sum(x => x.SubTotal);

        await _orderRepository
            .UpdateAsync(order);

        return new UpdateOrderItemQuantityResponse
        {
            Message = "Quantity updated"
        };
    }
}