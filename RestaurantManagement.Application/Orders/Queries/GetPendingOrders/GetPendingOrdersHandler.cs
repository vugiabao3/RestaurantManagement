using MediatR;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Orders.Queries.GetPendingOrders
{
    public class GetPendingOrdersHandler : IRequestHandler<GetPendingOrdersQuery, List<PendingOrderDto>>
    {
        private readonly IOrderRepository _repository;
        private readonly ICacheService _cache;

        public GetPendingOrdersHandler(IOrderRepository repository, ICacheService cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public Task<List<PendingOrderDto>> Handle(GetPendingOrdersQuery request, CancellationToken cancellationToken)
        {
            var key = $"pending-orders:{request.KitchenAreaId?.ToString() ?? "all"}";
            return _cache.GetOrCreateAsync(key, async () =>
            {
                var orders = await _repository.GetPendingOrdersAsync(request.KitchenAreaId);
                return orders.Select(order => MapToDto(order, request.KitchenAreaId)).ToList();
            }, TimeSpan.FromSeconds(15));
        }

        private static PendingOrderDto MapToDto(Order order, int? kitchenAreaId)
        {
            var details = order.OrderDetails.AsEnumerable();
            if (kitchenAreaId.HasValue)
                details = details.Where(x => x.MenuItem?.KitchenAreaId == kitchenAreaId.Value);

            return new PendingOrderDto
            {
                OrderId = order.OrderId,
                OrderNumber = order.OrderNumber,
                TableNumber = order.TableNumber,
                Status = order.Status,
                Priority = order.Priority,
                Version = order.Version,
                Created = order.Created,
                CanCompleteOrder = order.IsAllItemsReady(),
                Items = details.Select(detail => new OrderDetailItemDto
                {
                    OrderDetailId = detail.OrderDetailId,
                    MenuItemId = detail.MenuItemId,
                    MenuItemName = detail.MenuItem?.Name ?? string.Empty,
                    KitchenAreaId = detail.MenuItem?.KitchenAreaId,
                    KitchenAreaName = detail.MenuItem?.KitchenArea?.Name,
                    Quantity = detail.Quantity,
                    Note = detail.Note,
                    Status = detail.Status,
                    Version = detail.Version
                }).ToList()
            };
        }
    }
}
