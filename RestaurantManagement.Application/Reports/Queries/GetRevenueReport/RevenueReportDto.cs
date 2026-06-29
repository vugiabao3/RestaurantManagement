namespace RestaurantManagement.Application.Reports.Queries.GetRevenueReport;

public class RevenueReportDto
{
    public int Month { get; set; }
    public int Year { get; set; }
    public decimal TotalRevenue { get; set; }
    public int TotalDishesSold { get; set; }
    public List<TopSellingDishDto> TopSellingDishes { get; set; } = new();
}

public class TopSellingDishDto
{
    public int DishId { get; set; }
    public string DishName { get; set; } = string.Empty;
    public int QuantitySold { get; set; }
    public decimal TotalSales { get; set; }
}