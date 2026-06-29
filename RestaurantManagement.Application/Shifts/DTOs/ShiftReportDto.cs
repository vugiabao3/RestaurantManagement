using System;

namespace RestaurantManagement.Application.Shifts.DTOs;

public class ShiftReportDto
{
    public int ShiftId { get; set; }
    public int UserId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public decimal StartingCash { get; set; }
    public decimal EndingCash { get; set; }
    public decimal TotalRevenue { get; set; }
    public string Status { get; set; } = string.Empty;
}