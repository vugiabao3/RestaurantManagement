namespace RestaurantManagement.Application.Orders.Commands.AcceptOrder
{
    public class AcceptOrderResponse
    {
        public string Message { get; set; } = string.Empty;
        public int OrderId { get; set; }
        public string Status { get; set; } = string.Empty;
        public int Version { get; set; }
        public bool RealtimeSynchronized { get; set; }
    }
}
