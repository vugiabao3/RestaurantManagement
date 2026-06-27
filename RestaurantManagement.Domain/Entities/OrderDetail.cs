using RestaurantManagement.Domain.Enums;

namespace RestaurantManagement.Domain.Entities;

public class OrderDetail
{
    // Composite Key in Infrastructure, but defined as props here
    public int OrderId { get; set; }
    public int ItemId { get; set; }
    
    public int Quantity { get; set; }
    public decimal HistoricalPrice { get; set; }
    public ItemStatus ItemStatus { get; set; } = ItemStatus.Pending;
    public string? SpecialNote { get; set; }

    // Navigation Properties
    public CustomerOrder? Order { get; set; }
    public MenuItem? MenuItem { get; set; }
}