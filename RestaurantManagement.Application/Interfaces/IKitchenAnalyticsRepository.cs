using RestaurantManagement.Application.Analytics.Queries.GetKitchenAnalytics;

namespace RestaurantManagement.Application.Interfaces
{
    public interface IKitchenAnalyticsRepository
    {
        Task<KitchenAnalyticsResponse> GetSummaryAsync(DateTime from, DateTime to, int? kitchenAreaId);
    }
}
