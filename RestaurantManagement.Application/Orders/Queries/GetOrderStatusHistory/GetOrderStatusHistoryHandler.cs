using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.Orders.Queries.GetOrderStatusHistory
{
    public class GetOrderStatusHistoryHandler : IRequestHandler<GetOrderStatusHistoryQuery, List<StatusHistoryDto>>
    {
        private readonly IStatusHistoryRepository _repository;
        public GetOrderStatusHistoryHandler(IStatusHistoryRepository repository) => _repository = repository;

        public async Task<List<StatusHistoryDto>> Handle(GetOrderStatusHistoryQuery request, CancellationToken cancellationToken) =>
            (await _repository.GetByOrderIdAsync(request.OrderId)).Select(x => new StatusHistoryDto
            {
                StatusHistoryId = x.StatusHistoryId, OrderDetailId = x.OrderDetailId,
                OldStatus = x.OldStatus, NewStatus = x.NewStatus,
                ChangedByUserId = x.ChangedByUserId, ChangedAt = x.ChangedAt, Reason = x.Reason
            }).ToList();
    }
}
