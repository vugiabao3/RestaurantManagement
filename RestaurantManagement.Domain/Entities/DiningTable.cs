using RestaurantManagement.Domain.Enums;

namespace RestaurantManagement.Domain.Entities;

public class DiningTable
{
    public int TableId { get; set; }
    public string TableNumber { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public TableStatus CurrentStatus { get; set; } = TableStatus.Available;

    // Relationship
    public ICollection<CustomerOrder> Orders { get; set; } = new List<CustomerOrder>();
}