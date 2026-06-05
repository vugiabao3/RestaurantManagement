namespace RestaurantManagement.Domain.Entities;

public class OrderItem
{
    public int OrderItemId { get; set; }
    public int OrderId { get; set; }
    public int ItemId { get; set; }
    public int Quantity { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal DiscountRate { get; set; }
    public decimal UnitPrice { get; set; }
    public string ItemStatus { get; set; } = string.Empty;
    public bool IsDelayed { get; set; }
    public DateTime OrderedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public Order? Order { get; set; }
    public MenuItem? MenuItem { get; set; }
    public Notification? Notification { get; set; }
    public ICollection<KitchenDelayLog> DelayLogs { get; set; } = new List<KitchenDelayLog>();
}
