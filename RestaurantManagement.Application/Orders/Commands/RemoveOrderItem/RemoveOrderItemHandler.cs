using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.Orders.Commands.RemoveOrderItem;

public class RemoveOrderItemHandler
    : IRequestHandler<
        RemoveOrderItemCommand,
        RemoveOrderItemResponse>
{
    private readonly IOrderRepository _orderRepository;

    private readonly IOrderItemRepository _orderItemRepository;

    public RemoveOrderItemHandler(
        IOrderRepository orderRepository,
        IOrderItemRepository orderItemRepository)
    {
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
    }

    public async Task<RemoveOrderItemResponse> Handle(
        RemoveOrderItemCommand request,
        CancellationToken cancellationToken)
    {
        //--------------------------------------------------
        // Tìm OrderItem
        //--------------------------------------------------

        var item =
            await _orderItemRepository
                .GetByIdAsync(request.OrderItemId);

        if (item == null)
        {
            throw new Exception(
                "Order Item not found");
        }

        //--------------------------------------------------
        // Lấy Order
        //--------------------------------------------------

        var order =
            await _orderRepository
                .GetByIdAsync(item.OrderId);

        if (order == null)
        {
            throw new Exception(
                "Order not found");
        }

        //--------------------------------------------------
        // Cập nhật TotalAmount
        //--------------------------------------------------

        order.TotalAmount -= item.SubTotal;

        if (order.TotalAmount < 0)
        {
            order.TotalAmount = 0;
        }

        //--------------------------------------------------
        // Xóa OrderItem
        //--------------------------------------------------

        await _orderItemRepository
            .DeleteAsync(item);

        //--------------------------------------------------
        // Nếu giỏ hàng rỗng
        //--------------------------------------------------

        if (order.OrderItems.Count <= 1)
        {
            order.TotalAmount = 0;
        }

        //--------------------------------------------------
        // Save
        //--------------------------------------------------

        await _orderRepository
            .UpdateAsync(order);

        return new RemoveOrderItemResponse
        {
            Message = "Dish removed successfully"
        };
    }
}