namespace RestaurantManagement.Domain.Entities.Tracking;
public class RestaurantTable
{
    public int TableId { get; set; }
    public string TableNumber { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
