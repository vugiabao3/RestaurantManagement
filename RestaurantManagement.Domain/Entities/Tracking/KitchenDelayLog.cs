namespace RestaurantManagement.Domain.Entities;

public class KitchenDelayLog
{
    public int LogId { get; set; }
    public int OrderItemId { get; set; }
    public int ChefUserId { get; set; }
    public string DelayReason { get; set; } = string.Empty;
    public string DelayPriority { get; set; } = string.Empty;
    public int? DelayDuration { get; set; }
    public string? DelayNotes { get; set; }
    public bool IsAutoDetected { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime StartedAt { get; set; }
    public DateTime? ResolvedAt { get; set; }
    public OrderItem? OrderItem { get; set; }
    public AppUser? ChefUser { get; set; }
}
