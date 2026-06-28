namespace RestaurantManagement.Application.Orders.Commands.NotifyOrderCreated
{
    public class NotifyOrderCreatedResponse
    {
        public string Message { get; set; } = string.Empty;
        public int OrderId { get; set; }
        public bool RealtimeSynchronized { get; set; }
    }
}
