using MediatR;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Enums;

namespace RestaurantManagement.Application.Orders.Commands.UpdateOrderStatus;

public class UpdateOrderStatusHandler
    : IRequestHandler<UpdateOrderStatusCommand,
        UpdateOrderStatusResponse>
{
    private readonly IOrderRepository _orderRepository;

    public UpdateOrderStatusHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<UpdateOrderStatusResponse> Handle(
        UpdateOrderStatusCommand request,
        CancellationToken cancellationToken)
    {
        //--------------------------------------------------
        // 1. FIND ORDER
        //--------------------------------------------------

        var order = await _orderRepository.GetByIdAsync(request.OrderId);

        if (order == null)
            throw new Exception("Order not found");

        //--------------------------------------------------
        // 2. VALIDATE STATUS FLOW
        //--------------------------------------------------

        var current = order.Status;
        var next = request.Status;

        if (!IsValidTransition(current, next))
            throw new Exception($"Invalid status change: {current} → {next}");

        //--------------------------------------------------
        // 3. UPDATE STATUS
        //--------------------------------------------------

        order.Status = next;

        await _orderRepository.UpdateAsync(order);

        //--------------------------------------------------
        // 4. RESPONSE
        //--------------------------------------------------

        return new UpdateOrderStatusResponse
        {
            Message = "Order status updated",
            CurrentStatus = order.Status
        };
    }

    //--------------------------------------------------
    // BUSINESS RULE
    //--------------------------------------------------

    private bool IsValidTransition(string current, string next)
    {
        if (current == OrderStatus.Pending &&
            next == OrderStatus.Preparing)
            return true;

        if (current == OrderStatus.Preparing &&
            next == OrderStatus.Ready)
            return true;

        return false;
    }
}