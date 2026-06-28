using MediatR;

namespace RestaurantManagement.Application.Analytics.Queries.GetKitchenAnalytics
{
    public class GetKitchenAnalyticsQuery : IRequest<KitchenAnalyticsResponse>
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int? KitchenAreaId { get; set; }
    }
}
