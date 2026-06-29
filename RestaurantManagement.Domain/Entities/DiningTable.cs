namespace RestaurantManagement.Domain.Entities;

public enum TableStatus
{
    Available,
    Occupied,
    Reserved
}

public class DiningTable
{
    public int TableId { get; set; }
    public string TableNumber { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public TableStatus CurrentStatus { get; set; } = TableStatus.Available;

    public ICollection<CustomerOrder> Orders { get; set; } = new List<CustomerOrder>();
}