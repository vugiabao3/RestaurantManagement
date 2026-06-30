namespace RestaurantManagement.Domain.Entities;

public class Order
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public User? Customer { get; set; }

    public decimal TotalAmount { get; set; }

    public string Status { get; set; }
        = "Cart";

    public DateTime CreatedAt { get; set; }

    public ICollection<OrderItem> OrderItems
    {
        get;
        set;
    }
    = new List<OrderItem>();
    public int? MemberId { get; set; }

    public MemberCard? MemberCard { get; set; }

    public int TableId { get; set; }

    public DiningTable? Table { get; set; }
}