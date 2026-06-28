namespace RestaurantManagement.Application.Orders.Commands.CancelOrder
{
    public class CancelOrderResponse
    {
        public string Message { get; set; } = string.Empty;
        public int OrderId { get; set; }
        public string Status { get; set; } = string.Empty;
        public int Version { get; set; }
        public bool NotificationSent { get; set; }
    }
}
