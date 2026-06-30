using MediatR;

namespace RestaurantManagement.Application.Orders.Queries.GetPendingOrders;

public class GetPendingOrdersQuery
    : IRequest<List<GetPendingOrdersResponse>>
{
}