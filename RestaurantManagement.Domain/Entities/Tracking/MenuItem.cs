namespace RestaurantManagement.Domain.Entities;

public class MenuItem
{
    public int ItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public decimal Price { get; set; }
    public int CookingTimeStandard { get; set; }
    public string? Category { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime CreateAt { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
