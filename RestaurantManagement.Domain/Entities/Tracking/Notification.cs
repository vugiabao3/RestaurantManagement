namespace RestaurantManagement.Domain.Entities.Tracking;

public class Notification
{
    public int NotificationId { get; set; }
    public int OrderItemId { get; set; }
    public int TableId { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsDisplayed { get; set; }
    public string QueueStatus { get; set; } = string.Empty;
    public DateTime CreateAt { get; set; }
    public DateTime? DisplayedAt { get; set; }
    public OrderItem? OrderItem { get; set; }
    public RestaurantTable? Table { get; set; }
}
