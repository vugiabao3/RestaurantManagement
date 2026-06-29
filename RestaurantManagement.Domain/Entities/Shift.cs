using System;

namespace RestaurantManagement.Domain.Entities;

public enum ShiftStatus
{
    Active,
    Closed
}

public class Shift
{
    public int ShiftId { get; set; }
    public int UserId { get; set; }
    public DateTime StartTime { get; set; } = DateTime.Now;
    public DateTime? EndTime { get; set; }
    public decimal StartingCash { get; set; }
    public decimal EndingCash { get; set; }
    public decimal TotalRevenue { get; set; }
    public ShiftStatus Status { get; set; } = ShiftStatus.Active;
}