using MediatR;

namespace RestaurantManagement.Application.Orders.Queries.GetOrderStatusHistory
{
    public class GetOrderStatusHistoryQuery : IRequest<List<StatusHistoryDto>>
    {
        public int OrderId { get; set; }
    }

    public class StatusHistoryDto
    {
        public int StatusHistoryId { get; set; }
        public int? OrderDetailId { get; set; }
        public string OldStatus { get; set; } = string.Empty;
        public string NewStatus { get; set; } = string.Empty;
        public int? ChangedByUserId { get; set; }
        public DateTime ChangedAt { get; set; }
        public string? Reason { get; set; }
    }
}
