namespace RestaurantManagement.Application.Orders.Commands.UpdateOrderDetailStatus
{
    public class UpdateOrderDetailStatusRequest
    {
        public string Status { get; set; } = string.Empty;

        public int Version { get; set; }
    }
}
