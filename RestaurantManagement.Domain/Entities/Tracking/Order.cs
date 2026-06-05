namespace RestaurantManagement.Domain.Entities;

public class Order
{
    public int OrderId { get; set; }
    public int TableId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreateAt { get; set; }
    public DateTime? ClosedAt { get; set; }

    public RestaurantTable? Table { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
