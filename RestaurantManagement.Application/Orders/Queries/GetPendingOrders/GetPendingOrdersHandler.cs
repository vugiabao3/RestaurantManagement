using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.Orders.Queries.GetPendingOrders;

public class GetPendingOrdersHandler
    : IRequestHandler<GetPendingOrdersQuery,
        List<GetPendingOrdersResponse>>
{
    private readonly IOrderRepository _orderRepository;

    public GetPendingOrdersHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<List<GetPendingOrdersResponse>> Handle(
        GetPendingOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetPendingOrdersAsync();

        return orders.Select(o => new GetPendingOrdersResponse
        {
            OrderId = o.OrderId,
            TotalAmount = o.TotalAmount,
            Status = o.Status,
            CreatedAt = o.CreatedAt,

            Items = o.OrderItems.Select(i => new OrderItemDto
            {
                DishName = i.Dish?.Name ?? "",
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                SubTotal = i.SubTotal
            }).ToList()
        }).ToList();
    }
}