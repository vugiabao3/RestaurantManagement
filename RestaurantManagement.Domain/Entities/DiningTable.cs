using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Domain.Entities;

public class DiningTable
{
    [Key]
    public int TableId { get; set; }
    public string TableNumber { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public TableStatus CurrentStatus { get; set; } = TableStatus.Available;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
public enum TableStatus
{
    Available,
    Occupied,
    Reserved
}
