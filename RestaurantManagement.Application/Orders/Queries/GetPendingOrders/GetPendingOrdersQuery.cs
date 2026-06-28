using MediatR;

namespace RestaurantManagement.Application.Orders.Queries.GetPendingOrders
{
    public class GetPendingOrdersQuery : IRequest<List<PendingOrderDto>>
    {
        public int? KitchenAreaId { get; set; }
    }
}
