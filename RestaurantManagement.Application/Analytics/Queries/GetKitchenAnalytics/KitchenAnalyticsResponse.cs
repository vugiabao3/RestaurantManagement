namespace RestaurantManagement.Application.Analytics.Queries.GetKitchenAnalytics
{
    public class KitchenAnalyticsResponse
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int TotalOrders { get; set; }
        public int ReadyOrders { get; set; }
        public int CancelledOrders { get; set; }
        public int CompletedItems { get; set; }
        public double AveragePreparationMinutes { get; set; }
        public List<TopMenuItemDto> TopMenuItems { get; set; } = new();
        public List<KitchenAreaOrderCountDto> OrdersByArea { get; set; } = new();
    }

    public class KitchenAreaOrderCountDto
    {
        public int? KitchenAreaId { get; set; }
        public string KitchenAreaName { get; set; } = string.Empty;
        public int OrderCount { get; set; }
    }

    public class TopMenuItemDto
    {
        public int MenuItemId { get; set; }
        public string MenuItemName { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
