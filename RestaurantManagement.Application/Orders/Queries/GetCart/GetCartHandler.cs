using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.Orders.Queries.GetCart;

public class GetCartHandler : IRequestHandler<GetCartQuery, GetCartResponse>
{
    private readonly IOrderRepository _orderRepo;

    public GetCartHandler(IOrderRepository orderRepo)
    {
        _orderRepo = orderRepo;
    }

    public async Task<GetCartResponse> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepo.GetCartByCustomerAsync(request.CustomerId);

        if (order == null)
        {
            return new GetCartResponse();
        }

        return new GetCartResponse
        {
            OrderId = order.OrderId,
            TotalAmount = order.TotalAmount,
            Items = order.OrderItems.Select(x => new CartItemDto
            {
                OrderItemId = x.OrderItemId,
                DishId = x.DishId,
                // ✅ lấy name đúng cách
                DishName = x.Dish?.Name ?? "",
                UnitPrice = x.UnitPrice,
                Quantity = x.Quantity,
                SubTotal = x.SubTotal
            }).ToList()
        };
    }
}