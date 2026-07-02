using MediatR;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Orders.Commands.PlaceOrder;

public class PlaceOrderHandler
    : IRequestHandler<
        PlaceOrderCommand,
        PlaceOrderResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ITableRepository _tableRepository;

    public PlaceOrderHandler(
        IOrderRepository orderRepository,
        ITableRepository tableRepository)
    {
        _orderRepository = orderRepository;
        _tableRepository = tableRepository;
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
        //--------------------------------------------------
        // UPDATE TABLE STATUS
        //--------------------------------------------------

        var table = await _tableRepository.GetByIdAsync(order.TableId);

        if (table != null)
        {
            table.CurrentStatus = TableStatus.Occupied;

            await _tableRepository.UpdateAsync(table);
        }

        return new PlaceOrderResponse
        {
            Message =
                "Order sent to kitchen"
        };
    }
}