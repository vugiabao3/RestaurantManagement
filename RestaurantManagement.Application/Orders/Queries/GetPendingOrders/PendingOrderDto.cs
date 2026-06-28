namespace RestaurantManagement.Application.Orders.Queries.GetPendingOrders
{
    public class PendingOrderDto
    {
        public int OrderId { get; set; }
        public int OrderNumber { get; set; }
        public int TableNumber { get; set; }
        public string Status { get; set; } = string.Empty;
        public int Priority { get; set; }
        public int Version { get; set; }
        public DateTime Created { get; set; }
        public bool CanCompleteOrder { get; set; }
        public List<OrderDetailItemDto> Items { get; set; } = new();
    }
}
