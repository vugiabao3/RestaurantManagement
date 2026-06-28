namespace RestaurantManagement.Application.Orders.Queries.GetPendingOrders
{
    public class OrderDetailItemDto
    {
        public int OrderDetailId { get; set; }
        public int MenuItemId { get; set; }
        public string MenuItemName { get; set; } = string.Empty;
        public int? KitchenAreaId { get; set; }
        public string? KitchenAreaName { get; set; }
        public int Quantity { get; set; }
        public string? Note { get; set; }
        public string Status { get; set; } = string.Empty;
        public int Version { get; set; }
    }
}
