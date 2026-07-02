namespace RestaurantManagement.Application.Reports.Queries.RevenueByYear;

public class RevenueByYearResponse
{
    public int Year { get; set; }

    public decimal TotalRevenue { get; set; }

    public List<MonthlyRevenueDto> Months { get; set; } = new();
}

public class MonthlyRevenueDto
{
    public int Month { get; set; }

    public decimal Revenue { get; set; }
}