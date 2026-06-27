using RestaurantManagement.Domain.Enums;

namespace RestaurantManagement.Domain.Entities;

public class CustomerOrder
{
    public int OrderId { get; set; }
    public int TableId { get; set; }
    public DateTime StartTime { get; set; } = DateTime.Now;
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Unpaid;

    // Navigation Properties
    public DiningTable? DiningTable { get; set; }
    public Invoice? Invoice { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}