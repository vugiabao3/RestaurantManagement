namespace RestaurantManagement.Domain.Entities;

public class MenuItem
{
    public int ItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public decimal CurrentPrice { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsAvailable { get; set; } = true;

    // BỔ SUNG: Khóa ngoại theo đúng ERD
    public int CategoryId { get; set; } 
    public Category? Category { get; set; } 

    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}