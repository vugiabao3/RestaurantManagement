namespace RestaurantManagement.Domain.Entities;

public enum ItemStatus
{
    Pending,
    Cooking,
    Served,
    Cancelled
}

public class OrderDetail
{
    public int OrderId { get; set; }
    public int ItemId { get; set; }
    
    public int Quantity { get; set; }
    public decimal HistoricalPrice { get; set; }
    public ItemStatus ItemStatus { get; set; } = ItemStatus.Pending;
    public string? SpecialNote { get; set; }

    public CustomerOrder? Order { get; set; }
    public MenuItem? MenuItem { get; set; }
}