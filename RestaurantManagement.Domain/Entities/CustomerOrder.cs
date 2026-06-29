namespace RestaurantManagement.Domain.Entities;

public enum OrderStatus
{
    Unpaid,
    Paid,
    Cancelled
}

public class CustomerOrder
{
    public int OrderId { get; set; }
    public int TableId { get; set; }
    public DateTime StartTime { get; set; } = DateTime.Now;
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Unpaid;

    public DiningTable? DiningTable { get; set; }
    public Invoice? Invoice { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}