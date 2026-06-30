namespace RestaurantManagement.Application.Orders.Commands.UpdateOrderStatus;

public class UpdateOrderStatusResponse
{
    public string Message { get; set; } = string.Empty;

    public string CurrentStatus { get; set; } = string.Empty;
}