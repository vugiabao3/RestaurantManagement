namespace RestaurantManagement.Application.Orders.Commands.CompleteOrder
{
    public class CompleteOrderResponse
    {
        public string Message { get; set; } = string.Empty;
        public int OrderId { get; set; }
        public string Status { get; set; } = string.Empty;
        public int Version { get; set; }
        public DateTime? CompletedAt { get; set; }
        public bool StaffNotified { get; set; }
    }
}
